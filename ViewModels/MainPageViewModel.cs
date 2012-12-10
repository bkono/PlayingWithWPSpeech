using System;
using Caliburn.Micro;
using Windows.Phone.Speech.Synthesis;

namespace PlayingWithWPSpeech {
    public class MainPageViewModel : Screen {
        public MainPageViewModel() {
            _tts = new SpeechSynthesizer();
        }

        public String TimeOfDay { get; set; }
        public String VoiceCommandName { get; set; }
        public String Reco { get; set; }

        private string _name;
        private string _greeting;
        private SpeechSynthesizer _tts;

        public string Name {
            get { return _name; }
            set {
                _name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => CanSayHello);
            }
        }

        public string Greeting {
            get { return _greeting; }
            set {
                if (value == _greeting) return;
                _greeting = value;
                NotifyOfPropertyChange(() => Greeting);
            }
        }

        public bool CanSayHello {
            get { return string.IsNullOrEmpty(Name) == false; }
        }

        public async void SayHello() {
            if (String.IsNullOrEmpty(TimeOfDay))
                Greeting = "Hello " + Name;
            else Greeting = string.Format("Good {0} {1}", TimeOfDay, Name);

            await _tts.SpeakTextAsync(Greeting);
        }

        protected override async void OnViewReady(object view) {
            base.OnViewReady(view);

            if (string.IsNullOrEmpty(VoiceCommandName) == false) {
                switch (VoiceCommandName) {
                    case "greeting":
                        HandleGreetingLaunch();
                        break;
                }
            }

            if (String.IsNullOrEmpty(Name)) {
                Greeting = "It appears I don't know your name yet. Would you please enter it into the box?";
                await _tts.SpeakTextAsync(Greeting);
            }
        }

        private void HandleGreetingLaunch() {
            switch (TimeOfDay) {
                case "morning":
                    SetMorningResponse();
                    break;
                case "afternoon":
                    SetAfternoonResponse();
                    break;
                case "evening":
                    SetEveningResponse();
                    break;
            }
        }

        private async void SetEveningResponse() {
            var hour = DateTime.Now.Hour;
            if (hour < 6) {
                Greeting = "It's quite early isn't it?";
                TimeOfDay = "morning";
            }
            else if (hour >= 6 && hour < 12) {
                Greeting = "Seems to be a beautiful morning.";
                TimeOfDay = "morning";
            }
            else if (hour > 12 && hour < 17) {
                Greeting = "Actually it looks like afternoon to me " + Name;
                TimeOfDay = "afternoon";
            }
            else Greeting = "Good evening " + Name;

            await _tts.SpeakTextAsync(Greeting);
        }

        private async void SetAfternoonResponse() {
            var hour = DateTime.Now.Hour;
            if (hour < 6) {
                Greeting = "It's quite early isn't it?";
                TimeOfDay = "morning";
            }
            else if (hour >= 6 && hour < 12) {
                Greeting = "Seems to be a beautiful morning.";
                TimeOfDay = "morning";
            }
            else if (hour >= 17) {
                Greeting = "I'd say it is a great evening " + Name;
                TimeOfDay = "evening";
            }
            else Greeting = "Good afternoon " + Name;

            await _tts.SpeakTextAsync(Greeting);
        }

        private async void SetMorningResponse() {
            var hour = DateTime.Now.Hour;
            if (hour < 6) Greeting = "It's quite early isn't it?";
            else if (hour > 12 && hour < 17) {
                Greeting = "Actually it looks like afternoon to me " + Name;
                TimeOfDay = "afternoon";
            }
            else if (hour >= 17) {
                Greeting = "I'd say it is a great evening " + Name;
                TimeOfDay = "evening";
            }
            else Greeting = "Good morning " + Name;

            await _tts.SpeakTextAsync(Greeting);
        }
    }
}