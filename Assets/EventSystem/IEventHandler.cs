// Copyright (C) 2016 Filip Cyrus Bober

namespace FCB.EventSystem
{
    public interface IEventHandler
    {
        void SubscribeEvents();
        void UnsubscribeEvents();
    }
}