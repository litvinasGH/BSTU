   M S B A M L   `   `   `  ����F  BPrintinvest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Printinvest.ViewModelsPrintinvest.ViewModels  5   	   #    Printinvest.Views.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   3vm$clr-namespace:Printinvest.ViewModels     5   	     � Title$  Login��5   	   $	��250��6   $	��400��6$    � DataContext 5      +   %Printinvest.ViewModels.LoginViewModel  5   
   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��    	NameLogIN#���   6   $��18(�67   $
��Bold"�6E   $��Center=�6W   �� 5   
   $��1��6   $��0,10,0,0q�6"   .+�5      �� $	��200��6   $	��0,5q�6"   ��5      �� *Login+ �� UpdateSourceTrigger$ PropertyChanged=�G� 5      -   5        $	��200��5      $	��0,5q�6&   5      5      �� 5   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $
��0,10q�6X   .+�5      �� $��OK��6   $��80��6"   $��5q�6-   ��5      �� *LoginCommand+��66   ��  �� RelativeSource ��  �� AncestorType$ WindowL��� 5        Password_Cancel#���  6   $��80��6A   $��5q�6L   ��5      �� *CancelCommand+��67   ��  �� $ WindowL�5      5      5      