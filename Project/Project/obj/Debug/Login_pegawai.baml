   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ProjectProject  5   	       Project.Login_pegawai   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a?	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks   5   	   #localclr-namespace:Project   5   	   -   5          � Title$  Login_pegawai��5	   	   $	��450��6   $	��380��6,    � WindowStartupLocation$ CenterScreen=�5
   	    � AllowsTransparency . 
������  � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�6   .��5      � .+�5   
   �  � RowDefinitions 5      �� 5      �� 5      .+�5      ��  �� CornerRadius$ 10��6   $��2��6'   ��5      �� 5      .��5      �  � Color$ #7be9f6��6'    � Offset$	 0.1Q�67   � 5      $ 	#FF1B2029��6'   $	 0.7Q�69   5      5      �� 5       $��Center=�6   .+�5!      �� $��Image/logo.png��6   $	��150��60   $	��100��6<   $
��90,0q�6I   �� 5"      $��Login��6   $��Light"�5#      $��	helvetica)�5$      $��22(�5%      
�������$��Center=�5'      $��119,30,118,0q�68   5(      �� 5*      $��1��6   .+�5+      �� 	 �� Orientation$	 
Horizontal=�6   $��0,20,0,0q�66   .+�5,      �� -   5        
 ��Name$
 tbEmail��5,      $��	Helvetica)�6/   $��Light"�5-      $��Email / Username��5.      $��20(�5/      $��Center=�50      
�������
������ $��0q�53      $	��235��54      $
��Left=�55       J� Opacity$	 0.5Q�56      $��25��57      $��63,0,0,0q�58      6/   .  (MahApps.Metro.IconPacks.PackIconMaterial  59         Kind$ Account=�61   $��Center=�5:   0   $��Center=�5;   0   
�������5<   0   5=      �� 5>      $	��250��6   $��2��5?      
�������$	 0.5Q�5A      �� 5C      $	 
Horizontal=�6   $��0,20,0,0q�66   .+�5D      �� -   5        $
 tbPass��5D      $��	Helvetica)�6.   $��Light"�5E      $��Password��5F      $��20(�5G      $��Center=�5H      
�������
������ $��0q�5K      $	��235��5L      $
��Left=�5M      $	 0.5Q�5N      $��25��5O      $��63,0,0,0q�5P      6/     5Q      $ FormTextboxPassword=�61   $��Center=�5R   0   $��Center=�5S   0   
�������5T   0   5U      �� 5V      $	��250��6   $��2��5W      
�������$	 0.5Q�5Y      �� 5\      $	 
Horizontal=�6   $��0,35,0,0q�66   .+�5]      �� $ 6��6   
�������$��3q�6B   $��	30,0,60,0q�6V   $��Center=�6i   $	��325��6�   ._�5^      �� -   5         ��Name$ btnLoginPeg��5^   !   $��40��66   $��LOGIN��6B   $��	-3,0,-1,0q�6R   
�������
���������6�   Z� 6�   5_      5`      5a      5b      5c      5d      