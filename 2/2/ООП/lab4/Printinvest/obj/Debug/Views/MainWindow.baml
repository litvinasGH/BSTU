   M S B A M L   `   `   `  ����F  BPrintinvest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Printinvest.Views.ControlsPrintinvest.Views.Controls  5   	   ?$clr-namespace:Printinvest.ViewModelsPrintinvest.ViewModels  5   	   "    Printinvest.Views.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   =controls(clr-namespace:Printinvest.Views.Controls     5   	   3vm$clr-namespace:Printinvest.ViewModels     5   	      AppTitle  � Title#  �   5   	   $	��768��5   	   $
��1366��6    � Icon$ /Views/printinvest.png��5   	    � Cursor$&  /Assets/Icons/printer_cursor.cur��5	   	     W#��[ 5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � DataContext 5      *   $Printinvest.ViewModels.MainViewModel  5   
    J� InputBindings 5      �� 5   
    �� Gesture$ 
Ctrl+Alt+A��6    �� Command 5      �� *UnlockAdminCommand+.��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $	��300 �6   �� 5      $��* �6   .+�5   
   ,   &Printinvest.Views.Controls.FilterPanel  $��0��6   ]� 5    
   $��1��6   $��10,0,0,0q�6$   .+�5!      �� $	��Top=�6   	 �� Orientation$	 
Horizontal=�6.   $��0,0,0,10q�6G   $��Right=�6Y   .+�5#      �� $��EN��6   $��0,0,5,0q�5%      
 � ToolTip$
 Switch to English��5&      ��5$      �� *SwitchToEnglishCommand+5&      �� 5'      $��RU��6   $��0,0,20,0q�5)      $0
 *Переключить на русский��5*      ��5(      �� *SwitchToRussianCommand+5*      �� 5-      $��А��6     FloatingActionButton#��[ 5/        AddNewButton#
 �  50      ��5.      �� *UnlockAdminCommand+50      �� 51      $	��➕��6   #��[ 53      #
 �  55      ��52      �� *OpenAdminPanelCommand+y�54      �� *IsAdminUnlocked+   BooleanToVisibilityConverter �� 	Converter# [ 55      �� 56      $	��↩��6   #��[ 58      #
 �  5:      ��57      �� *UndoCommand+y�59      �� *IsAdminUnlocked+# [ 5:      �� 5;      $	��↪��6   #��[ 5=      #
 �  5?      ��5<      �� *RedoCommand+y�5>      �� *IsAdminUnlocked+# [ 5?      5A      �� 5C      $
��Auto=�6   .��5D      �� ��6   �� *CurrentItems+��5E      �� 5F      .R�5G      �  � Orientation$ 
Horizontal=�6(   $��Center=�5H   (    � 	ItemWidth$	 220��5I   (    � 
ItemHeight$	 350��5J   (   5K      ��5M      �� 5N      .R�5P      �� .+�5R   "   ��   CardButtonStyle#��[ 5U      ��5S      �� *)'DataContext.ShowEquipmentDetailsCommand+ �� RelativeSource ��  �� AncestorType$ WindowL���5T      �� .��5V   &   .   (Printinvest.Views.Controls.EquipmentCard  5W   #   �� 5Z   "   $	 
Horizontal=�5[      $��0,10,0,0q�5\      y�5]      �� *DataContext.IsAdminUnlocked+ �� $ WindowL�# [ .+�5`   &   �� $��Изменить��5a      $��0,0,5,0q�5e      ��5b      �� *" DataContext.EditEquipmentCommand+ �� $ WindowL���5d      �� 5e      �� 5f   &   $��Удалить��5g      ��5h      �� *$"DataContext.DeleteEquipmentCommand+ �� $ WindowL���5j      �� 5k   #   5l      5m      5o      5p      5q      5r      5s      