   M S B A M L   `   `   `  ����  Printinvest+    %Printinvest.Views.EquipmentEditWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	      
Edit_Title  � Title#  �   5   	     W#��[ 5   	   $	��300��5   	   $	��400��6    � WindowStartupLocation$ CenterOwner=�5   	   .��5      � $��10q�6    � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� .+�5      ��   
Edit_Model#���  6   �� 5        ModernTextBox#��[ 6R   ��6   �� *Model+ �� UpdateSourceTrigger$ PropertyChanged=�6R   �� 5        
Edit_Price#���  6   $��0,10,0,0q�5      �� 5      #��[ 6R   ��6   �� *Price+$ PropertyChanged=�6R   �� 5        
Edit_Brand#���  6   $��0,10,0,0q�5      �� 5        FilterComboBox#��[ 6?   ��6   �� *AvailableBrands+ �� SelectedItem 5      �� *Brand+5      �� 5   
   $��2��6    �� Orientation$ 
Horizontal=�5      $��Right=�5      $��0,10,0,0q�5       .+�5!      �� -   5          	Edit_Save#���  5!      $��80��5"      $��0,0,10,0q�5#       �� 	IsDefault .   AnimatedButton#��[ 5%      5&      �� 5'      -   5         	 Edit_Cancel#��� 	 5'      $��80��5(       �� IsCancel . #��[ 5*      5+      5,      5-      5.      