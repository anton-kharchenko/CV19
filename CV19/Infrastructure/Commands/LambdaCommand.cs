﻿using System;
using CV19.Infrastructure.Commands.Base;

namespace CV19.Infrastructure.Commands
{
    public class LambdaCommand : Command
    {
        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecute;

        public LambdaCommand(Action<object> Execute,  Func<object, bool> CanExecute =  null)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute;
        }

        public override void Execute(object parameter) => _Execute(parameter);

        public override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;

    }
}