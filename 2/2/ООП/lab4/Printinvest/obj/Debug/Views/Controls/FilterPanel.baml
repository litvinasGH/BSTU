   M S B A M L   `   `   `  ����F  BPrintinvest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Printinvest.Models.EnumsPrintinvest.Models.Enums  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      /clr-namespace:CustomControlsCustomControls  5      ,    &Printinvest.Views.Controls.FilterPanel   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      8enums&clr-namespace:Printinvest.Models.Enums     5      6sys&clr-namespace:System;assembly=mscorlib     5      -custclr-namespace:CustomControls     5        � 	Resources  5         BrandValues S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� TypeName  ServiceTypeValues��%�   &	        &	 p     S� 5	   
   $ 	GetValues��5
      "  5       5      M� 5      $ enums:Brand��6   5      S� 5   
   $ 	GetValues��5      "  5       5      M� 5      $ enums:ServiceType��6   5      5      .��5      �� $��10q�6   .+�5   
   ��   Filter_Search#���  6   �� 5   
     ModernTextBox#��[ 6T   $��0,5,0,10q�5      ��5      �� *SearchQuery+ �� UpdateSourceTrigger$ PropertyChanged=�5      �� 5   
     Filter_Brand#���  6   �� 5   
   $��0,5,0,10q�5!      ��5      ��  �� Source# [   �� SelectedItem 5       �� *SelectedBrand+5!      �� 5#   
     Filter_ServiceType#���  6   �� 5$   
   $��0,5,0,10q�5&      ��5$      �� # [  5%      �� *SelectedServiceType+5&      �� 5(   
     Filter_MaxPrice#���  6   �� 5)   
    �� Orientation$ 
Horizontal=�6   $��0,5,0,0q�6.   .+�5*      "   CustomControls.NumericUpDown  
	   Min$	 0��5+      

   Max$
 100000��6   $	��150��6&      Value 5*   !   �� *
MaxPrice+$ PropertyChanged=�5+   &   5.      5/      50      