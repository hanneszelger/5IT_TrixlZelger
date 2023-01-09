namespace SocialMediaApp;

public partial class page_inchats : ContentPage
{
	public page_inchats()
	{
		InitializeComponent();
		lbl_name.Text = page_messages.name;
	}
}