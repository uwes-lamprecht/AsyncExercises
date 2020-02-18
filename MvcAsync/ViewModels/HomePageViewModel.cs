using System;
using System.Collections.Generic;

namespace MvcAsync.ViewModels
{
    public class HomePageViewModel
    {
        public List<string> Messages { get; set; }

        public void AddMessage(string message)
        {
            if (Messages == null)
            {
                Messages = new List<string>();
            }

            Messages.Add(message);
        }
    }
}