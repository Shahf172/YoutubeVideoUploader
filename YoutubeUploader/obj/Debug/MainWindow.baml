   M S B A M L   `   `   `  ����J  FYoutubeUploader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:YoutubeUploaderYoutubeUploader  5   	        YoutubeUploader.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:YoutubeUploader     5   	   -   5          � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $��50 �6   �� 5      $��60 �6   �� 5      $��60 �6   �� 5      $��60 �6   �� 5      $��60 �6   .+�5   
   �� $��30��6    �� Orientation$ 
Horizontal=�6!   $��0��6:   $��Center=�6G   $
��Left=�6b   .+�5      �� $��Choose Video:��6   6.   �� 5      -   5         ��Name$ txtPath��5      {�.  $	��350��6:   6H   �� 5      -   5        $��+��5      $��20(�6!   $��20��6D   6Q   5      �� 5   
   $��30��6   $ 
Horizontal=�6!   $��1��6:   $��Center=�6G   $
��Left=�6b   .+�5      �� $��File Name:      ��6   61   �� 5      -   5        $ txtFileName��5       �� TextAlignment$ Center=�6+   $	��150��6B   6P   5      �� 5   
   $��30��6   $ 
Horizontal=�6!   $��2��6:   $��Center=�6G   $
��Left=�6b   .+�5      �� $��File Desc:       ��6   62   �� 5      -   5        $ txtFileDesc��5      $ Center=�6+   $	��350��6B   6P   5      �� 5   
   $��30��6   $ 
Horizontal=�6!   $��3��6:   $��Center=�6G   $
��Left=�6b   .+�5      �� $��Tag:                ��6   65   �� 5       -   5        $ 
txtFileTag��5       $ Center=�6*   $	��350��6A   6O   �� 5!      -   5        $��Upload��5!      $��25 0 0 0q�6&   $	��100��68   $��4��6D   $��30��6Q   $��Center=�6]   6�   5"      �� 5#   
   -   5         ��Name$ 	lblstatus��5#      $��4��6#   $
��Left=�60   6M   5$      5%      