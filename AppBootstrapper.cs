using Microsoft.Phone.Shell;
using Windows.Phone.Speech.VoiceCommands;

namespace PlayingWithWPSpeech {
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using Microsoft.Phone.Controls;
    using Caliburn.Micro;

    public class AppBootstrapper : PhoneBootstrapper {
        private PhoneContainer container;

        protected override void Configure() {
            container = new PhoneContainer(RootFrame);

            container.RegisterPhoneServices();
            container.PerRequest<MainPageViewModel>();

            AddCustomConventions();
        }

        protected override object GetInstance(Type service, string key) {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            container.BuildUp(instance);
        }

        private static void AddCustomConventions() {
            ConventionManager.AddElementConvention<Pivot>(Pivot.ItemsSourceProperty, "SelectedItem", "SelectionChanged")
                             .ApplyBinding =
                (viewModelType, path, property, element, convention) => {
                    if (ConventionManager
                        .GetElementConvention(typeof (ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention)) {
                        ConventionManager
                            .ConfigureSelectedItem(element, Pivot.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Pivot.HeaderTemplateProperty, null, viewModelType);
                        return true;
                    }

                    return false;
                };

            ConventionManager.AddElementConvention<Panorama>(Panorama.ItemsSourceProperty, "SelectedItem",
                                                             "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) => {
                    if (ConventionManager
                        .GetElementConvention(typeof (ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention)) {
                        ConventionManager
                            .ConfigureSelectedItem(element, Panorama.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Panorama.HeaderTemplateProperty, null, viewModelType);
                        return true;
                    }

                    return false;
                };
        }

        protected override void OnLaunch(object sender, LaunchingEventArgs e) {
            RegisterVoiceCommands();
            base.OnLaunch(sender, e);
        }

        private async void RegisterVoiceCommands()
        {
            await VoiceCommandService.InstallCommandSetsFromFileAsync(
                new Uri("ms-appx:///VoiceCommands.xml", UriKind.RelativeOrAbsolute));
        }
    }
}