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

        public Game ActiveGame { get; set; }

        private TimeSpan TimeLeft { get; set; }

        protected int CurrentQuestion { get; set; }


        Timer timer;

        public GameService(IHubContext<QuizHub> hubContext)
        {
            _hubContext = hubContext;

            ActiveGame = new Game();

            ActiveGame.State = GameState.NotScheduled;

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

        public void StartGame()
        {
            ActiveGame.Quiz = Quiz.CreateTestQuiz();

            ActiveGame.State = GameState.WaitingForStart;
            CurrentQuestion = 0;

            TimeLeft = TimeSpan.FromSeconds(30);
        }

        private void UpdateQuizState(object sender, ElapsedEventArgs e)
        {
            if(ActiveGame.Quiz == null)
            {
                return;
            }

            if (TimeLeft.TotalSeconds > 0)
            {
                TimeLeft = TimeLeft.Subtract(TimeSpan.FromSeconds(1));
            }
            else
            {
                if (ActiveGame.State == GameState.WaitingForStart)
                {
                    ActiveGame.State = GameState.QuestionAsked;
                    ActiveGame.ActiveQuestion = ActiveGame.Quiz.QuizQuestions[0].CreateQuestion();
                    TimeLeft = TimeSpan.FromSeconds(10);
                }
                else if (ActiveGame.State == GameState.QuestionAsked)
                {
                    ActiveGame.State = GameState.QuestionResults;

                    foreach (var player in ActiveGame.Players)
                    {
                        var answer = player.answersHistory.FirstOrDefault(x => x.QuestionId == ActiveGame.ActiveQuestion.QuestionId);

                        if (answer == null)
                        {
                            answer = new AnswerRecord(ActiveGame.ActiveQuestion.QuestionId, 0);
                            player.answersHistory.Add(answer);
                        }

                        answer.IsCorrect = answer.AnswerId == ActiveGame.ActiveQuestion.CorrectAnswerId;
                    }

                    ActiveGame.ActiveQuestion.Answers.First(x => x.Id == ActiveGame.ActiveQuestion.CorrectAnswerId).IsCorrect = true;

                    TimeLeft = TimeSpan.FromSeconds(10);
                }
                else if (ActiveGame.State == GameState.QuestionResults)
                {
                    if (CurrentQuestion < ActiveGame.Quiz.QuizQuestions.Count - 1)
                    {
                        ActiveGame.State = GameState.QuestionAsked;
                        CurrentQuestion++;
                        ActiveGame.ActiveQuestion = ActiveGame.Quiz.QuizQuestions[CurrentQuestion].CreateQuestion();
                        TimeLeft = TimeSpan.FromSeconds(10);
                    }
                    else
                    {
                        ActiveGame.State = GameState.QuizResults;
                    }
                }
            }

            _hubContext.Clients.All.SendAsync("UpdateQuizState", new GameNotification(ActiveGame.State, TimeLeft));
        }

        public List<AnswerRecord> ValidateRespond(Guid playerId, int answerId)
        {
            var player = ActiveGame.Players.FirstOrDefault(it => it.Id == playerId);

            if (ActiveGame.State == GameState.QuestionAsked)
            {
                if (!player.answersHistory.Any(x => x.QuestionId == ActiveGame.ActiveQuestion.QuestionId))
                {
                    player.answersHistory.Add(new AnswerRecord(ActiveGame.ActiveQuestion.QuestionId, answerId));
                    ActiveGame.ActiveQuestion.Answers.First(x => x.Id == answerId).responsesCount++;
                }
            }
            return player.answersHistory;
        }
    }
}
