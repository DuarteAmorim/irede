﻿namespace irede.shared.Notifications
{
    public class Notification
    {
        //public string Property { get; private set; }

        public string Message { get; private set; }

        public Notification(string message)
        {
            //Property = property;
            Message = message;
        }
    }
}
