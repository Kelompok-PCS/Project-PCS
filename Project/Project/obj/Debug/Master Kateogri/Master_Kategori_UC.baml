   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ProjectProject  5           Project.Master_Kategori_UC   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:Project     5      
�������.��5
      � -   5        .+�5   
   �� -   5          ��Name$  	btnFilter��5      $��	593,3,0,0q�6$   $
��Left=�67   $	��Top=�6R   $��56��6j   $��34��6u    K� ZIndex$ 1��6�   .��5      �� $7��1pack://siteoforigin:,,,/Resources/search-icon.png��6   $��22��6O   $��4,0,6,0q�6[   $ 0��6l   5      �� 5   
   -   5         ��Name$ tbFilter��5      $
��Left=�6$   $��34��6?   $��	458,4,0,0q�6K    �� TextWrapping$
 Wrap=�6^   $	��Top=�6r   $	��135��6�   �� 5   
   $��	Filter By��6   $
��Left=�6$   $��	400,3,0,0q�6?   $	��Top=�6R   ���	#FFE0A800$��33��6�   $��59��6�   �� 5   
   -   5        $  	btnInsert��5      $��Insert New Kategori��6$   $
��Left=�6B   $��15,3,0,0q�6]   $	��Top=�6o   $	��137��6�   ���	#FF0069D9���	#FFF7EFEF$��34��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dtgKategori��5      $
��Left=�6(   $	��401��6C   $��
-1,120,0,0q�6P   $	��Top=�6d   $	��651��6|   6�     5   
   -   5        $ purgKategori��5      $
��Left=�6)   $	��401��6D   $��	0,664,0,0q�6Q   $	��Top=�6d   $	��651��6|   6�   �� 5   
   -   5         ��Name$ lbKode��5      $��Id Kategori��6    $
��Left=�66   $��
23,556,0,0q�6Q   $	��Top=�6e   �� 5   
   -   5        $ tbNama��5      $
��Left=�6"   $��23��6=   $��102,559,0,0q�6I   $
 Wrap=�6^   $��Kosong��6r   $	��Top=�6�   $	��120��6�   {�.  6�   �� 5   
   -	   5        $  btnEdit��5      $
��Edit��6"   $
��Left=�61   $��365,560,0,0q�6L   $	��Top=�6a   $��75��6y   6�   	� 5   
   -
   5         	�Name$ rdDelete��5      $��Delete��6(   $
��Left=�69   $��292,563,0,0q�6T   $	��Top=�6i    	� 	GroupName$ option��6�   6�   	� 5   
   -   5        $ rdUpdate��5      $
��Edit��6(   $
��Left=�67   $��236,563,0,0q�6R   $	��Top=�6g   $ option��6    d� 	IsChecked$
 TrueY�6�   6�   �� 5   
   -   5        $ lbKodePurge��5      $��Id Kategori��6%   $
��Left=�6;   $��23,1084,0,0q�6V   $	��Top=�6k   �� 5   
   -   5        $ tbNamaPulih��5      $
��Left=�6'   $��23��6B   $��103,1087,0,0q�6N   $
 Wrap=�6d   $��Kosong��6x   $	��Top=�6�   $	��120��6�   {�.  6�   �� 5   
   -   5        $  btnPulihkan��5      $��Pulihkan��6&   $
��Left=�69   $��237,1090,0,0q�6T   $	��Top=�6j   $��75��6�   6�   �� 5   
   ���	#FF004CF0$
��Left=�6%   $��28��6@   $��	-1,89,0,0q�6L   
���   �$	��Top=�6n   $	��800��6�   �� 5   
   $��Kategori Aktif��6   $
��Left=�6)   $��	-1,86,0,0q�6D   $	��Top=�6W   $	��126��6o   $��18(�6{   
�������6�   �� 5   
   ���	#FFF50D0D$
��Left=�6%   $��28��6@   $��0,633,-49,0q�6L   
���   �$	��Top=�6p   $	��800��6�   �� 5   
   $��Kategori Tidak Aktif��6   $
��Left=�6/   $��	0,630,0,0q�6J   $	��Top=�6]   $	��190��6u   $��18(�6�   
�������6�   5       5#      