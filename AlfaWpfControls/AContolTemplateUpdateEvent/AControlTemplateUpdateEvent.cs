using System.Windows;
using System.Windows.Controls;

namespace AlfaWpfControls.AContolTemplateUpdateEvent
{
    public class AControlTemplateUpdateEvent : Control
    {
        public static readonly RoutedEvent UpdateTemplateEvent = EventManager.RegisterRoutedEvent("UpdateTemplate",
            RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(AControlTemplateUpdateEvent));

        public event RoutedEventHandler UpdateTemplate
        {
            add { AddHandler(UpdateTemplateEvent, value); }
            remove { RemoveHandler(UpdateTemplateEvent, value); }
        }

        private void RaiseUpdateTemplateEvent()
        {
            RaiseEvent(new RoutedEventArgs(UpdateTemplateEvent));
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            RaiseUpdateTemplateEvent();
        }


    }
}
