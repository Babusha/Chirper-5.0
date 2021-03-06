﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirper_5._0.Models
{
    public class MySubscribtionList
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ApplicationUser CurrentUser;

        public List<ApplicationUser> Subscriptions;

        public MySubscribtionList(ApplicationUser currentUser)
        {
            CurrentUser = currentUser;
            Subscriptions = currentUser.Subscriptions.ToList();
        }
    }
}

