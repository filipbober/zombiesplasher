// Copyright (C) 2016 Filip Cyrus Bober

namespace ZombieSplasher
{
    public interface IActorController
    {
        /// <summary>
        /// Initialize object and set it active
        /// </summary>
        void Initialize();
        void Deactivate();
    }
}