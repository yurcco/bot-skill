// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Extensions.Configuration;
using Microsoft.Bot.Schema;

namespace Microsoft.BotBuilderSamples.SkillBot.Dialogs
{
    public class TestActionDialog : ComponentDialog
    {

        public TestActionDialog(IConfiguration configuration)
            : base(nameof(TestActionDialog))
        {

            var steps = new WaterfallStep[] {
                SendSuggestedActionsAsync
            };

            AddDialog(new WaterfallDialog(nameof(TestActionDialog), steps));
        }
        private static async Task<DialogTurnResult> SendSuggestedActionsAsync(WaterfallStepContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("What is your favorite color?");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                {
                    new CardAction() { Title = "Red", Type = ActionTypes.ImBack, Value = "Red", Image = "https://via.placeholder.com/20/FF0000?text=R",},
                    new CardAction() { Title = "Yellow", Type = ActionTypes.ImBack, Value = "Yellow", Image = "https://via.placeholder.com/20/FFFF00?text=Y"},
                    new CardAction() { Title = "Blue", Type = ActionTypes.ImBack, Value = "Blue", Image = "https://via.placeholder.com/20/0000FF?text=B"},
                },
            };
            await turnContext.Context.SendActivityAsync(reply, cancellationToken);
            return await turnContext.EndDialogAsync(null, cancellationToken);
        }
    }
}
