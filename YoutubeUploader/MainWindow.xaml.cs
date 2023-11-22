using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Threading;
using System.Reflection;
using Google.Apis.Auth.OAuth2;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;
using Google.Apis.YouTube.v3.Data;
using System.Text.RegularExpressions;
using Google.Apis.Upload;
using System.Windows.Threading;

namespace YoutubeUploader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            //openFileDialog1.Filter = "Database files (*.mdb, *.accdb)|*.mdb;*.accdb";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            string selectedFileName = "";
            if (openFileDialog1.ShowDialog() == true)
            {
                selectedFileName = openFileDialog1.FileName;
                //...
            }
            txtPath.Text = selectedFileName;
        }

        private async Task Run()
        {
            await this.Dispatcher.Invoke(async () =>
            {
                UserCredential credential;
                using (var stream = new FileStream("client_id.json", FileMode.Open, FileAccess.Read))
                {
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        // This OAuth 2.0 access scope allows an application to upload files to the
                        // authenticated user's YouTube channel, but doesn't allow other types of access.
                        new[] { YouTubeService.Scope.YoutubeUpload },
                        "user",
                        CancellationToken.None
                    );
                }

                YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
                });

                var video = new Video();
                video.Snippet = new VideoSnippet();
                video.Snippet.Title = txtFileName.Text;
                video.Snippet.Description = txtFileDesc.Text;
                string[] tagSeo = Regex.Split(txtFileTag.Text, ",");
                video.Snippet.Tags = tagSeo;
                video.Snippet.CategoryId = "22"; // See https://developers.google.com/youtube/v3/docs/videoCategories/list
                video.Status = new VideoStatus();
                video.Status.PrivacyStatus = "public"; // or "private" or "public"
                var filePath = txtPath.Text; // Replace with path to actual movie file.

                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                    videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
                    videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;

                    await videosInsertRequest.UploadAsync();
                }
            });
            
        }

        void videosInsertRequest_ProgressChanged(Google.Apis.Upload.IUploadProgress progress)
        {
            switch (progress.Status)
            {
                case UploadStatus.Uploading:
                    this.Dispatcher.Invoke(() =>
                    {
                        lblstatus.Content = String.Format("{0} bytes sent.", progress.BytesSent);
                    });
                    break;

                case UploadStatus.Failed:
                    this.Dispatcher.Invoke(() =>
                    {
                        lblstatus.Content = String.Format("An error prevented the upload from completing.\n{0}", progress.Exception);
                    });
                    break;
            }
        }

        void videosInsertRequest_ResponseReceived(Video video)
        {
            this.Dispatcher.Invoke(() =>
            {
                lblstatus.Content = string.Format("Video id '{0}' was successfully uploaded.", video.Id);
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lblstatus.Content = "uploading video....";
            try
            { 
                Thread thead = new Thread(() =>
                {
                    Run().Wait();
                });
                thead.IsBackground = true;
                thead.Start();

            }
            catch (AggregateException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
