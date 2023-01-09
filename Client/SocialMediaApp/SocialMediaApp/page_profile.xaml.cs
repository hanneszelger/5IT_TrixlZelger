using Microsoft.Maui.Controls;

namespace SocialMediaApp;

public partial class page_profile : ContentPage
{
    public static int Row = 2;
    public static int Col = 1;
    public static int likedRow = 2;
    public static int likedCol = 1;
    RowDefinition rowDef1;
    List<ImageButton> imageButtons = new List<ImageButton>();
    List<ImageButton> likedImages = new List<ImageButton>();
    List<Label> labels = new List<Label>();
    List<string> comments = new List<string>(){"gut", "schön", "Toll"};
    

    public page_profile()
	{
		InitializeComponent();
        
    }

	private void btn_mypics_Clicked(object sender, EventArgs e)
	{
	//if tab is switched to user pics show them and hide the comments
        foreach (ImageButton ib in imageButtons)
        {
            ib.IsVisible = true;
        }
        
        foreach (Label label in labels)
        {
            label.IsVisible = false;
        }

        btn_addpics.IsVisible = true;
	}

    private async void btn_profilepicture_Clicked(object sender, EventArgs e)
	{
	//if user clicks on his profilpicture user will get to choose a picture with the PickAndShow method
        var res = await PickAndShow(PickOptions.Default);
        btn_profilepicture.Source = res.FullPath;
        
    }

    private void btn_comments_Clicked(object sender, EventArgs e)
	{
	//adds comments
        int commentrow = 2;
        btn_addpics.IsVisible=false;

        foreach (ImageButton ib in imageButtons)
        {
            ib.IsVisible = false;
        }

        foreach(string comment in comments)
        {
            Label c = new Label
            {
                Text = comment,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            grid_maingrid.Add(c);
            labels.Add(c);
            grid_maingrid.SetRow(c, commentrow);
            grid_maingrid.SetColumnSpan(c, 3);
            commentrow++;
        }
	}

	private void btn_likedpics_Clicked(object sender, EventArgs e)
	{
        btn_addpics.IsVisible = false;
        ImageButton imageButton = new ImageButton
        {

            Source = "plus.png",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.CenterAndExpand

        };
        likedImages.Add(imageButton);

        foreach (ImageButton ib in imageButtons)
        {
            ib.IsVisible = false;
        }

        foreach (Label label in labels)
        {
            label.IsVisible = false;
        }

        foreach(ImageButton li in likedImages.ToList())
        {
	    //Images get added to a list the grid defines one more row if there are already 3 in one row
            grid_maingrid.Add(li);
            likedImages.Add(li);
            grid_maingrid.SetColumn(li, likedCol);
            grid_maingrid.SetRow(li, likedRow);
            likedCol++;

            if (likedCol % 3 == 0)
            {
                rowDef1 = new RowDefinition { Height = 100 };
                grid_maingrid.RowDefinitions.Add(rowDef1);
                likedRow++;
                likedCol = 0;
            }
        }

    }

    

    private async void btn_addpics_Clicked(object sender, EventArgs e)
    {
        //Images get added to a list the grid defines one more row if there are already 3 in one row
        var res = await PickAndShow(PickOptions.Default);
        ImageButton imageButton = new ImageButton
        {
            
            Source = res.FullPath,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.CenterAndExpand
            
        };
        grid_maingrid.Add(imageButton);
        imageButtons.Add(imageButton);
        grid_maingrid.SetColumn(imageButton, Col);
        grid_maingrid.SetRow(imageButton, Row);
        Col++;

        if(Col % 3 == 0)
        {
            rowDef1 = new RowDefinition { Height = 100};
            grid_maingrid.RowDefinitions.Add(rowDef1);
            Row++;
            Col = 0;
        }
    }

    public async Task<FileResult> PickAndShow(PickOptions options)
    {
    //method to pick pictures
        try
        {
            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                {
		//uses the stream to get the image if it is an image
                    using var stream = await result.OpenReadAsync();
                    var image = ImageSource.FromStream(() => stream);
                }
            }
            //returns all info of the pic
            return result;
        }
        catch (Exception ex)
        {
            // The user canceled or something went wrong
        }

        return null;
    }

    public void hideunhide(bool change)
    {
    //method to hide controls
        btn_addpics.IsVisible = change;
        foreach (ImageButton ib in imageButtons)
        {
            ib.IsVisible = change;
        }

        foreach (Label label in labels)
        {
            label.IsVisible = change;
        }
        foreach (ImageButton li in likedImages)
        {
            li.IsVisible = change;
        }
    }
}
