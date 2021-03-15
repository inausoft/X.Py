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
    public class QuizService
    {
        private readonly IHubContext<QuizHub> hubContext;

        public QuizSource ActiveQuiz { get; private set; }

        public Question ActiveQuestion { get; set; }

        private TimeSpan TimeLeft { get; set; }

        public QuizState State { get; set; }

        protected int CurrentQuestion { get; set; }

        public readonly IPlayerService playerService;

        Timer timer;

        public QuizService(IHubContext<QuizHub> hubContext, IPlayerService playerService)
        {
            this.hubContext = hubContext;

            State = QuizState.NotScheduled;

            this.playerService = playerService;

            timer = new Timer(1000);
            timer.Start();

            timer.Elapsed += UpdateQuizState;
        }

        public void StartQuiz()
        {
            ActiveQuiz = QuizSource.CreateTestQuiz();

            State = QuizState.WaitingForStart;
            CurrentQuestion = 0;

            TimeLeft = TimeSpan.FromMinutes(1);
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
                if (State == QuizState.WaitingForStart)
                {
                    State = QuizState.QuestionAsked;
                    ActiveQuestion = ActiveQuiz.QuizQuestions[0].CreateQuestion();
                    TimeLeft = TimeSpan.FromSeconds(10);
                }
                else if (State == QuizState.QuestionAsked)
                {
                    State = QuizState.QuestionResults;

                    foreach (var player in playerService.GetAllPlayers())
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
                else if (State == QuizState.QuestionResults)
                {
                    if (CurrentQuestion < ActiveQuiz.QuizQuestions.Count - 1)
                    {
                        State = QuizState.QuestionAsked;
                        CurrentQuestion++;
                        ActiveQuestion = ActiveQuiz.QuizQuestions[CurrentQuestion].CreateQuestion();
                        TimeLeft = TimeSpan.FromSeconds(10);
                    }
                    else
                    {
                        State = QuizState.QuizResults;
                    }
                }
            }

            hubContext.Clients.All.SendAsync("UpdateQuizState", new GameNotification(State, TimeLeft));
        }

        public List<AnswerRecord> ValidateRespond(Guid playerId, int answerId)
        {
            var player = playerService.GetPlayer(playerId);

            if (State == QuizState.QuestionAsked)
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
