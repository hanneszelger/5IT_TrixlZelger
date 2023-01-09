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
        var res = await PickAndShow(PickOptions.Default);
        btn_profilepicture.Source = res.FullPath;
        
    }

    private void btn_comments_Clicked(object sender, EventArgs e)
	{
        int commentrow = 2;
        btn_addpics.IsVisible=false;

        foreach (ImageButton ib in imageButtons)
        {
            ib.IsVisible = false;
        }
        //grid_maingrid.RowDefinitions.
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
        try
        {
            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = await result.OpenReadAsync();
                    var image = ImageSource.FromStream(() => stream);
                }
            }

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