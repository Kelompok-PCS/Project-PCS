   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ProjectProject  5      h dSAPBusinessObjects.WPF.Viewer, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304tRclr-namespace:SAPBusinessObjects.WPF.Viewer;assembly=SAPBusinessObjects.WPF.ViewerSAPBusinessObjects.WPF.Viewer 5          Project.Laporan   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:Project     5      eViewerRclr-namespace:SAPBusinessObjects.WPF.Viewer;assembly=SAPBusinessObjects.WPF.Viewer     5      
�������.��5      � .+�5   
   �� $��Laporan��6   $
��Left=�6"   $��	10,10,0,0q�6=   $	��Top=�6P   $	��110��6h   $��22(�6t   $
��Bold"�6�   (  "System.Windows.Controls.DatePicker  5   
   -   5           Name$  inptgl��5      $
��Left=�6%   $��30��6@   $��	10,90,0,0q�6L   $	��Top=�6_   $	��285��6w     5   
   -   5        $  inpbln��5      $
��Left=�6%   $��30��6@   $��
320,90,0,0q�6L   $	��Top=�6`   $	��321��6x   �� 5   
   $��Masukkan Tanggal��6   $
��Left=�6+   $��	10,59,0,0q�6F   $	��Top=�6Y   $	��155��6q   $��16(�6}   $
��Bold"�6�   �� 5   
   $��Masukkan Bulan��6   $
��Left=�6)   $��
320,59,0,0q�6D   $	��Top=�6X   $	��155��6p   $��16(�6|   $
��Bold"�6�   	� 5   
   -   5         	�Name$	 rb1��5      $��Harian��6#   $
��Left=�64   $��
10,165,0,0q�6O   $	��Top=�6c   6{   	� 5   
   -   5        $	 rb3��5      $��Bulanan��6#   $
��Left=�65   $��
10,185,0,0q�6P   $	��Top=�6d   6|   �� 5   
   $��Laporan Transaksi��6   $
��Left=�6,   $��
10,140,0,0q�6G   $	��Top=�6[   $	��115��6s   $
��Bold"�6   	� 5   
   -   5        $	 rb4��5      $��Harian��6#   $
��Left=�64   $��260,165,0,0q�6O   $	��Top=�6d   6|   	� 5   
   -   5        $	 rb5��5      $��Bulanan��6#   $
��Left=�65   $��260,185,0,0q�6P   $	��Top=�6e   6}   �� 5   
   $��Menu Favorit��6   $
��Left=�6'   $��260,140,0,0q�6B   $	��Top=�6W   $��85��6o   $
��Bold"�6z   �� 5   
   -   5         ��Name$ print��5      $��Cetak Laporan��6    $
��Left=�68   $��25��6S   $��
10,230,0,0q�6_   $	��Top=�6s   $	��634��6�   6�   	� 5   
   -   5        $	 rb6��5      $��Pembeli (member)��6#   $
��Left=�6>   $��475,166,0,0q�6Y   $	��Top=�6n   6�   	� 5   
   -	   5        $	 rb7��5      $!��Penjualan (jenis pemesanan)��6#   $
��Left=�6I   $��475,185,0,0q�6d   $	��Top=�6y   6�   �� 5   
   $��	Terbanyak��6   $
��Left=�6$   $��475,140,0,0q�6?   $	��Top=�6T   $��75��6l   $
��Bold"�6w   8  2SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer  5   
   -
   5          Name$ viewer��5   &   $
��Left=�66   $	��530��6Q   $��
10,260,0,0q�6^   $	��Top=�6r   $	��631��6�   5      5      