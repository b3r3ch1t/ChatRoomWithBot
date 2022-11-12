﻿using ChatRoomWithBot.Domain.Interfaces;

namespace ChatRoomWithBot.Domain.Entities
{
    public abstract class  Entity<T> : IEntity where T : IEntity
    {
        public Guid Id { get; protected set; }
        public bool Valid { get; protected set; }
        public DateTime DateCreated { get; protected  set; }
       

        public void ChangeDateCreated(DateTime date)
        {
            DateCreated = date;
        }

        public void Activate()
        {
            Valid = true;
        }

        public void Deactivate()
        {
            Valid = false ;
        }

        public void ChangeId()
        {
            Id = Guid.NewGuid();
        }
    }
}
