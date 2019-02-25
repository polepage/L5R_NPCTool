﻿using Microsoft.Win32;
using NPC.Presenter.Windows.Dialogs;
using Prism.Services.Dialogs;
using System;

namespace NPC.Presenter.Windows.Extensions
{
    static class DialogServiceExtension
    {
        public static void ShowSaveDialog(this IDialogService service, IDialogParameters parameters, Action<IDialogResult> callback)
        {
            var saveDialog = new SaveFileDialog
            {
                Title = parameters.GetValue<string>(Dialog.Title),
                Filter = parameters.GetValue<string>(Dialog.SaveFile.Filter),
                ValidateNames = true
            };

            var dialogResult = new DialogResult(saveDialog.ShowDialog());
            if (dialogResult.Result.GetValueOrDefault())
            {
                dialogResult.Parameters.Add(Dialog.SaveFile.Target, saveDialog.FileName);
            }

            callback?.Invoke(dialogResult);
        }
    }
}
