using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Telemedic.Templates;
using Telemedic.Templates.Enums;

namespace Telemedic.Controller
{
    static class NotificationController
    {
        public static async Task LoadNotifications(Dictionary<int, List<Object>> NotificationData, StackLayout ParentStack)
        {
            //parent stack not empty, update existing views if different
            if (ParentStack.Children.Count > 0)
            {
                if (ParentStack.Children.Count == 1 && ParentStack.Children[0].GetType() == typeof(Label)) ParentStack.Children.Clear();
                foreach (int Key in NotificationData.Keys)
                {
                    bool MadeChanges = false;
                    for (int i = 0; i <  ParentStack.Children.Count; i++)
                    {
                        var Child = ParentStack.Children[i];
                        String[] ChildTag = ControlTagger<String>.GetTag(Child).Split('-');
                        if (ChildTag[0] == NotificationData[Key][3].ToString() && ChildTag[1] == NotificationData[Key][1].ToString())
                        {
                            Device.BeginInvokeOnMainThread(delegate 
                            {
                                ParentStack.Children.RemoveAt(i);

                                var ChildStack = NotificationTemplate.Notification01("", NotificationData[Key][0].ToString(), Utilities.GetValidatedTime(DateTime.Parse(NotificationData[Key][1].ToString()), ChatSectionType.ChatList), (NotificationType)NotificationData[Key][2], (int)NotificationData[Key][3]);
                                ControlTagger<String>.SetTag(ChildStack, NotificationData[Key][3].ToString() + "-" + NotificationData[Key][1].ToString());
                                ParentStack.Children.Add(Child);
                            });
                            MadeChanges = true;
                            break;
                        }
                    }

                    if (!MadeChanges)
                    {
                        Device.BeginInvokeOnMainThread(delegate 
                        {
                            var ChildStack = NotificationTemplate.Notification01("", NotificationData[Key][0].ToString(), Utilities.GetValidatedTime(DateTime.Parse(NotificationData[Key][1].ToString()), ChatSectionType.ChatList), (NotificationType)NotificationData[Key][2], (int)NotificationData[Key][3]);
                            ControlTagger<String>.SetTag(ChildStack, NotificationData[Key][3].ToString() + "-" + NotificationData[Key][1].ToString());
                            ParentStack.Children.Add(ChildStack);
                        });
                    }
                }
                
            }
            else
            {
                foreach (int Key in NotificationData.Keys)
                {
                    Device.BeginInvokeOnMainThread(() => 
                    {
                        var Child = NotificationTemplate.Notification01("", NotificationData[Key][0].ToString(), Utilities.GetValidatedTime(DateTime.Parse(NotificationData[Key][1].ToString()), ChatSectionType.ChatList), (NotificationType)NotificationData[Key][2], (int)NotificationData[Key][3]);
                        ControlTagger<String>.SetTag(Child, NotificationData[Key][3].ToString() + "-" + NotificationData[Key][1].ToString());
                        ParentStack.Children.Add(Child);
                    });
                }
            }

            if (NotificationData.Count == 0 && ParentStack.Children.Count == 0)
            {
                Device.BeginInvokeOnMainThread(() => 
                {
                    ParentStack.Children.Add(new Label { Text = "No recent notifications.", Style = App.Current.Resources["_EmptyLabelTemplate"] as Style });
                });
            }
        }
    }
}
