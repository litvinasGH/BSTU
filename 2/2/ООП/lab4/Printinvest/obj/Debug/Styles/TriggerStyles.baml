   M S B A M L   `   `   `  ����F  BPrintinvest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Printinvest.UtilsPrintinvest.Utils  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089(    "Printinvest.Utils.IsAtMaxConverter   IsAtMaxConverter  ReactiveSlider  �� 
TargetType �� Property  PrimaryBrush �� Value �� Triggers �� Binding �� RelativeSource �� Value N� EnterActions �� TargetProperty		 �� To
 g� Duration g� AutoReverse �� Path �� 	Converter �� ConverterParameter  ValidatedTextBox  AnimatedButton  TextPrimaryBrush �� 
TargetType ��Name �� CornerRadius �� Triggers :� RoutedEvent �� 
TargetName	 W� To �� ScaleX �� ScaleY�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      9i3http://schemas.microsoft.com/expression/interaction  5      @ei9http://schemas.microsoft.com/expression/2010/interactions  5      (convclr-namespace:Printinvest.Utils  5      %  &	        &	      7  7   &	 U    7  &	 �    7  7     5      6   �� 5
      "  ��6#   .�5   
   ��  � ��8   6(   �� 5   
    � ��$ 1q�6,    5   
   �� 5       6   �� *Value+ �� *Self+ 06Z   .a�5      ��  �@�ToolTip$' !Минимум достигнут��6,    5      �� 5      .��5      �� .��5      �� $2 ,(Border.BorderBrush).(SolidColorBrush.Color)�6-   $		 RedX�5      $
 0:0:0.3D�6(    . 6;   5      5      5      �� 5       5   	   ��  �� *Self+$ Value�8   �� *Self+ True5"   	   .a�5#      ��  �@�ToolTip$) #Максимум достигнут��6,    5$      �� 5%      .��5&      �� .��5'      �� $2 ,(Border.BorderBrush).(SolidColorBrush.Color)�6-   $	 GreenX�5(      $
 0:0:0.3D�6*    . 6=   5)      5*      5,      5.      �� 51      "  ��6%   .�52   
   ��  � ��8   6(   �� 53   
    � ��$ 1q�6,    54   
   �� 55       6   �� *Text.Length+ �� *Self+ 06`   .a�56      ��  � ��
 � E��60    57      �� 59      .��5:      �� 5<      5=      5B      5D      �� 5G      "  ��6#   .�5H   
   ��  � ��8   6'   �� 5I   
    � ��8  6'   �� 5J   
    � �� 5K      �� 5L      " ��6"   .R�5M      �� $ border��6   #��z��6-   $ 4��5N      ._�5O      �� $��Center=�6+   $��Center=�5P      5Q       5R      :� 5S      $ Click��6'   .[�5T      �� .��5U   "   �� .��5W   &   W� $ border��66   $- 'RenderTransform.(ScaleTransform.ScaleX)�5X   $   $	 1.1X�5Y   $   $
 0:0:0.1D�6-    . 6@   W� 5Z   &   $ border��66   $- 'RenderTransform.(ScaleTransform.ScaleY)�5[   $   $	 1.1X�5\   $   $
 0:0:0.1D�6-    . 6@   5]   #   5^      5_      5a      5c      �� 5d   
    �@J�RenderTransformOrigin$ 0.5,0.5&�62   �� 5e   
    � z� 5f      �� 5g      $ 1Q�6!   $ 1Q�6,   5i      5j      5l      