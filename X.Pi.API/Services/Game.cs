using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;
using X.Pi.API.Models;
using Microsoft.AspNetCore.SignalR;
using X.Pi.API.Controllers;
using X.Pi.API.Hubs;
using X.Pi.API.Services.Interfaces;

namespace X.Pi.API.Services
{
    public class Game
    {
        private readonly IHubContext<QuizHub> hubContext;

        public event EventHandler<GameNotification> QuizUpdated;

        public List<Quiz> Quizes { get; private set; }

        public Quiz ActiveQuiz { get; private set; }

        public Question ActiveQuestion { get; set; }

        private TimeSpan TimeLeft { get; set; } = TimeSpan.FromMinutes(1);

        public GameState State { get; set; }

        public int CurrentQuestion { get; set; }

        public readonly IPlayerService playerService;

        Timer timer;

        public Game(IHubContext<QuizHub> hubContext, IPlayerService playerService)
        {
            this.hubContext = hubContext;

            State = GameState.NotScheduled;

            this.playerService = playerService;

            Quizes = new List<Quiz>();
            Quizes.Add(Quiz.CreateTestQuiz());
        }

        public void StartQuiz()
        {
            ActiveQuiz = Quizes[0];

            timer = new Timer(1000);
            timer.Start();
            timer.Elapsed += UpdateQuizState;

            State = GameState.WaitingForStart;
        }

        private void UpdateQuizState(object sender, ElapsedEventArgs e)
        {
            if(TimeLeft.TotalSeconds > 0)
            {
                TimeLeft = TimeLeft.Subtract(TimeSpan.FromSeconds(1));
            }
            else
            {
                if(State == GameState.WaitingForStart)
                {
                    State = GameState.QuestionAsked;
                    ActiveQuestion = ActiveQuiz.QuizQuestions[0].CreateQuestion();
                    TimeLeft = TimeSpan.FromSeconds(10);
                }
                else if (State == GameState.QuestionAsked)
                {
                    State = GameState.QuestionResults;

                    foreach(var player in playerService.participants)
                    {
                        var answer = player.answersHistory.FirstOrDefault(x => x.QuestionId == ActiveQuestion.QuestionId);

                        if(answer == null)
                        {
                            answer = new AnswerRecord(ActiveQuestion.QuestionId, 0);
                            player.answersHistory.Add(answer);
                        }

                        answer.IsCorrect = ActiveQuestion.CorrectAnswerId == answer.AnswerId;
                    }

                    ActiveQuestion.Answers.First(x => x.Id == ActiveQuestion.CorrectAnswerId).IsCorrect = true;

                    TimeLeft = TimeSpan.FromSeconds(10);
                }
                else if (State == GameState.QuestionResults)
                {
                    if (CurrentQuestion < ActiveQuiz.QuizQuestions.Count - 1)
                    {
                        State = GameState.QuestionAsked;
                        CurrentQuestion++;
                        ActiveQuestion = ActiveQuiz.QuizQuestions[CurrentQuestion].CreateQuestion();
                        TimeLeft = TimeSpan.FromSeconds(10);
                    }
                    else
                    {
                        State = GameState.QuizResults;
                    }
                }
            }

            hubContext.Clients.All.SendAsync("UpdateQuizState", new GameNotification(State, TimeLeft));
        }

        //public Guid RegisterPlayer(string name)
        //{
        //    Guid playerToken = Guid.NewGuid();

        //    participants.Add(new Player(playerToken, name));

        //    return playerToken;
        //}

        public List<AnswerRecord> ValidateRespond(Guid playerId, int answerId)
        {
            var player = playerService.GetPlayer(playerId);

            if (State == GameState.QuestionAsked)
            {
                if (!player.answersHistory.Any(x => x.QuestionId == ActiveQuestion.QuestionId))
                {
                    player.answersHistory.Add(new AnswerRecord(ActiveQuestion.QuestionId, answerId));
                    ActiveQuestion.Answers.First(x => x.Id == answerId).responsesCount++;
                }

                //if(!Statistics.Any(x => x.Player == player && x.Question == Quiz.ActiveQuestion))
                //{
                //    Statistics.Add(new Answerrr(player, Quiz.ActiveQuestion, answerId, QuestionTimeLimit.Subtract(TimeLeft)));
                //}
            }
            return player.answersHistory;
        }

       

        //public Player GetPlayer(Guid id)
        //{
        //   return participants.First(x => x.Id == id);
        //}
    }
}
