   M S B A M L   `   `   `  ����F  BPrintinvest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Printinvest.ViewsPrintinvest.Views  5   	   %    Printinvest.Views.ProfileWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   1localclr-namespace:Printinvest.Views     5   	      W#��[  5   	     ProfileWindowTitle  � Title#  �  5   	   $	��400��66   $	��400��6C   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $��* �6   �� 5      $
��Auto �6   .+�5   
   ��   UserProfileHeader#���  6   $��18(�6?   $
��Bold"�6M   $��Center=�6_   $��0,0,0,10q�6|   �� 5   
   $��1��6   $��0,0,0,10q�6"   .+�5      �� $	��0,5q�6     ModernTextBox#��[ 6#     NamePlaceholder
 � Tag# �  5      ��5      �� *	User.Name+ �� UpdateSourceTrigger$ PropertyChanged=�5      �� 5      $	��0,5q�6   #��[ 6#     LoginPlaceholder# �  5      ��5      �� *
User.Login+$ PropertyChanged=�5      G� 5      $	��0,5q�6     ModernTextBoxP#��[ 6'    5      �� *User.Password+ �� Mode$ TwoWay=�$ PropertyChanged=�5      �� 5   
   $��2��6    �� Orientation$ Vertical=�6"   $��Center=�69   .+�5      ��   
SaveButton#���  6   $	��100��6<   $	��0,5q�6h     AnimatedButton#��[ 6u   ��6H   �� *SaveCommand+6u   �� 5      $ 
Horizontal=�6   $��Center=�62   $��0,10,0,0q�6O   .+�5      �� $��EN��6   $��40��6&   $��5q�61   $��en-US��6f   #��[ 6   ��6<   �� *SwitchLanguageCommand+6   �� 5       $��RU��6   $��40��6&   $��5q�61   $��ru-RU��6f   #��[ 6   ��6<   �� *SwitchLanguageCommand+6   5!      �� 5#      $ 
Horizontal=�6   $��Center=�62   $��0,10,0,0q�6O   .+�5$      ��  	 
LightTheme#��� 	 6   $��60��6@   $��5q�6K   $��
LightTheme��6}   #��[ 6�   ��6V   �� *ToggleThemeCommand+6�   �� 5%       
 	DarkTheme#��� 
 6   $��60��6?   $��5q�6J   $��	DarkTheme��6|   #��[ 6�   ��6U   �� *ToggleThemeCommand+6�   5&      5'      5(      5)      