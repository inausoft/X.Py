using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;
using X.Pi.API.Models;
using Microsoft.AspNetCore.SignalR;
using X.Pi.API.Controllers;
using X.Pi.API.Hubs;

namespace X.Pi.API.Services
{
    public class GameService
    {
        private readonly IHubContext<QuizHub> _hubContext;

        public Quiz ActiveQuiz { get; private set; }

        public Question ActiveQuestion { get; set; }

        public Game ActiveGame { get; set; }

        private TimeSpan TimeLeft { get; set; }

        public GameState State { get; set; }

        protected int CurrentQuestion { get; set; }


        Timer timer;

        public GameService(IHubContext<QuizHub> hubContext)
        {
            _hubContext = hubContext;

            ActiveGame = new Game();

            State = GameState.NotScheduled;

            timer = new Timer(1000);
            timer.Start();

            timer.Elapsed += UpdateQuizState;
        }

        public Guid RegisterPlayer(string name)
        {
            Guid playerToken = Guid.NewGuid();

            ActiveGame.Players.Add(new Player(playerToken, name));

            _hubContext.Clients.All.SendAsync("PlayersCountChnaged", ActiveGame.Players.Count);

            return playerToken;
        }

        public void StartQuiz()
        {
            ActiveQuiz = Quiz.CreateTestQuiz();

            State = GameState.WaitingForStart;
            CurrentQuestion = 0;

            TimeLeft = TimeSpan.FromSeconds(30);
        }

        private void UpdateQuizState(object sender, ElapsedEventArgs e)
        {
            if(ActiveQuiz == null)
            {
                return;
            }

            if (TimeLeft.TotalSeconds > 0)
            {
                TimeLeft = TimeLeft.Subtract(TimeSpan.FromSeconds(1));
            }
            else
            {
                if (State == GameState.WaitingForStart)
                {
                    State = GameState.QuestionAsked;
                    ActiveQuestion = ActiveQuiz.QuizQuestions[0].CreateQuestion();
                    TimeLeft = TimeSpan.FromSeconds(10);
                }
                else if (State == GameState.QuestionAsked)
                {
                    State = GameState.QuestionResults;

                    foreach (var player in ActiveGame.Players)
                    {
                        var answer = player.answersHistory.FirstOrDefault(x => x.QuestionId == ActiveQuestion.QuestionId);

                        if (answer == null)
                        {
                            answer = new AnswerRecord(ActiveQuestion.QuestionId, 0);
                            player.answersHistory.Add(answer);
                        }

                        answer.IsCorrect = answer.AnswerId == ActiveQuestion.CorrectAnswerId;
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

            _hubContext.Clients.All.SendAsync("UpdateQuizState", new GameNotification(State, TimeLeft));
        }

        public List<AnswerRecord> ValidateRespond(Guid playerId, int answerId)
        {
            var player = ActiveGame.Players.FirstOrDefault(it => it.Id == playerId);

            if (State == GameState.QuestionAsked)
            {
                if (!player.answersHistory.Any(x => x.QuestionId == ActiveQuestion.QuestionId))
                {
                    player.answersHistory.Add(new AnswerRecord(ActiveQuestion.QuestionId, answerId));
                    ActiveQuestion.Answers.First(x => x.Id == answerId).responsesCount++;
                }
            }
            return player.answersHistory;
        }
    }
}
