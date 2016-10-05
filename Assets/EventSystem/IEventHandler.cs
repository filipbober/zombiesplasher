// Copyright (C) 2016 Filip Cyrus Bober

namespace EventSystem
{
    public interface IEventHandler
    {
        void SubscribeEvents();
        void UnsubscribeEvents();
    }
}