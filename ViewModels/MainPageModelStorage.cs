using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayingWithWPSpeech.ViewModels
{
    public class MainPageModelStorage : StorageHandler<MainPageViewModel>
    {
        public override void Configure() {
            Property(x => x.Name)
                .InAppSettings()
                .RestoreAfterActivation();
        }
    }
}
