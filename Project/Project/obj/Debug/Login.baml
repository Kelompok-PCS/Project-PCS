   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ProjectProject  5   	       Project.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a?	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks   5   	   #localclr-namespace:Project   5   	   -   5          � Title$  Login��5	   	   $	��450��6   $	��380��6$    � WindowStartupLocation$ CenterScreen=�5
   	    � AllowsTransparency . 
������  � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�6   .��5      � .+�5   
   �  � RowDefinitions 5      �� 5      �� 5      .+�5      ��  �� CornerRadius$ 10��6   $��2��6'   ��5      �� 5      .��5      �  � Color$ #7be9f6��6'    � Offset$	 0.1Q�67   � 5      $ 	#FF1B2029��6'   $	 0.7Q�69   5      5      �� 5!      $��Center=�6   .+�5"      �� $��Image/logo.png��6   $	��150��60   $	��100��6<   $
��90,0q�6I   �� 5#      $��Login Database��6   $��Light"�5$      $��	helvetica)�5%      $��22(�5&      
�������$��Center=�5(      $��119,30,118,0q�68   5)      �� 5+      $��1��6   .+�5,      �� 	 �� Orientation$	 
Horizontal=�6   .+�5-      �� -   5        
 ��Name$
 tbData��5-      $��	Helvetica)�6.   $��Light"�5.      $��Source��5/      $��20(�50      $��Center=�51      
�������
������ $��0q�54      $	��235��55      $
��Left=�56       J� Opacity$	 0.5Q�57      $��25��58      $��63,0,0,0q�59      6N   .  (MahApps.Metro.IconPacks.PackIconMaterial  5;         Kind$ 
SourceFork=�61   $��Center=�5<   0   $��Center=�5=   0   
�������5>   0   5?      �� 5@      $	��250��6   $��2��5A      
�������$	 0.5Q�5C      �� 5G      $	 
Horizontal=�6   $��0,20,0,0q�66   .+�5H      �� -   5        $
 tbUser��5H      $��	Helvetica)�6.   $��Light"�5I      $��Username��5J      $��20(�5K      $��Center=�5L      
�������
������ $��0q�5O      $	��235��5P      $
��Left=�5Q      $	 0.5Q�5R      $��25��5S      $��63,0,0,0q�5T      6N     5U      $ Account=�61   $��Center=�5V   0   $��Center=�5W   0   
�������5X   0   5Y      �� 5Z      $	��250��6   $��2��5[      
�������$	 0.5Q�5]      �� 5_      $	 
Horizontal=�6   $��0,20,0,0q�66   .+�5`      �� -   5        $
 tbPass��5`      $��	Helvetica)�6.   $��Light"�5a      $��Password��5b      $��20(�5c      $��Center=�5d      
�������
������ $��0q�5g      $	��235��5h      $
��Left=�5i      $	 0.5Q�5j      $��25��5k      $��63,0,0,0q�5l      6N     5m      $ FormTextboxPassword=�61   $��Center=�5n   0   $��Center=�5o   0   
�������5p   0   5q      �� 5r      $	��250��6   $��2��5s      
�������$	 0.5Q�5u      �� 5x      $	 
Horizontal=�6   $��0,35,0,0q�66   .+�5y      �� $ 6��6   
�������$��3q�6B   $��	30,0,60,0q�6V   $��Center=�6i   $	��325��6�   ._�5z      �� -   5         ��Name$ btnLogin��5z   !   $��40��63   $��LOGIN��6?   $��	-3,0,-1,0q�6O   
�������
���������6�   Z� 6�   5{      5|      5}      5~      5      5�      