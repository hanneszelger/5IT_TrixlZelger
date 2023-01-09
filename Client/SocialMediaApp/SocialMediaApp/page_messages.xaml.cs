namespace SocialMediaApp;
using Microsoft.Maui.Controls;
public partial class page_messages : ContentPage
{
    public static string name;
    public page_messages()
    {
        InitializeComponent();
    }

    private async void btn_newmsg_ClickedAsync(object sender, EventArgs e)
    {
        name = await DisplayPromptAsync("Who shall be messaged?", "Name of your friend:");
        await Navigation.PushAsync(new page_inchats());
    }
}