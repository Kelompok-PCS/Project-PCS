   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Project.Master_menuProject.Master_menu  5      )    #Project.Master_menu.Menu_makanan_UC   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:Project.Master_menu     5      
�������$��1142.081��5   %   .��5	      � -   5        .+�5
   
   �� -   5          ��Name$  	btnFilter��5
      $��	593,3,0,0q�6$   $
��Left=�67   $	��Top=�6R   $��56��6j   $��34��6u    K� ZIndex$ 1��6�   .��5      �� $7��1pack://siteoforigin:,,,/Resources/search-icon.png��6   $��22��6O   $��4,0,6,0q�6[   $ 0��6l   5      �� 5   
   -   5         ��Name$ tbFilter��5      $
��Left=�6$   $��34��6?   $��	458,4,0,0q�6K    �� TextWrapping$
 Wrap=�6^   $	��Top=�6r   $	��135��6�   �� 5   
   $��	Filter By��6   $
��Left=�6$   $��	400,3,0,0q�6?   $	��Top=�6R   ���	#FFE0A800$��33��6�   $��59��6�   �� 5   
   -   5        $  	btnInsert��5      $��Insert New Menu��6$   $
��Left=�6>   $��15,3,0,0q�6Y   $	��Top=�6k   $	��137��6�   ���	#FF0069D9���	#FFF7EFEF$��34��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ gridMenu��5      $
��Left=�6%   $	��401��6@   $��
-1,120,0,0q�6M   $	��Top=�6a   $	��651��6y   6�     5   
   -   5        $ gridPurgatory��5      $
��Left=�6*   $	��401��6E   $��	0,664,0,0q�6R   $	��Top=�6e   $	��651��6}   6�   �� 5   
   -   5         ��Name$ lbKode��5      $��Id Menu��6    $
��Left=�62   $��
23,556,0,0q�6M   $	��Top=�6a   �� 5   
   -   5        $ tbNama��5      $
��Left=�6"   $��23��6=   $��102,559,0,0q�6I   $
 Wrap=�6^   $��Kosong��6r   $	��Top=�6�   $	��120��6�   {�.  6�   �� 5   
   -	   5        $  btnEdit��5      $
��Edit��6"   $
��Left=�61   $��365,560,0,0q�6L   $	��Top=�6a   $��75��6y   6�   	� 5   
   -
   5         	�Name$ rdDelete��5      $��Delete��6(   $
��Left=�69   $��292,563,0,0q�6T   $	��Top=�6i    	� 	GroupName$ option��6�   6�   	� 5   
   -   5        $ rdUpdate��5      $
��Edit��6(   $
��Left=�67   $��236,563,0,0q�6R   $	��Top=�6g   $ option��6    d� 	IsChecked$
 TrueY�6�   6�   �� 5   
   -   5        $ lbKodePurge��5      $��Id Menu��6%   $
��Left=�67   $��23,1084,0,0q�6R   $	��Top=�6g   �� 5   
   -   5        $ tbNamaPulih��5      $
��Left=�6'   $��23��6B   $��103,1087,0,0q�6N   $
 Wrap=�6d   $��Kosong��6x   $	��Top=�6�   $	��120��6�   {�.  6�   �� 5   
   -   5        $  btnPulihkan��5      $��Pulihkan��6&   $
��Left=�69   $��237,1090,0,0q�6T   $	��Top=�6j   $��75��6�   6�   �� 5   
   ���	#FF004CF0$
��Left=�6%   $��28��6@   $��	-1,89,0,0q�6L   
���   �$	��Top=�6n   $	��800��6�   �� 5   
   $��
Menu Aktif��6   $
��Left=�6%   $��	-1,86,0,0q�6@   $	��Top=�6S   $	��105��6k   $��18(�6w   
�������6�   �� 5   
   ���	#FFF50D0D$
��Left=�6%   $��28��6@   $��0,633,-49,0q�6L   
���   �$	��Top=�6p   $	��800��6�   �� 5   
   $��Menu Tidak Aktif��6   $
��Left=�6+   $��	0,630,0,0q�6F   $	��Top=�6Y   $	��158��6q   $��18(�6}   
�������6�   5      5       