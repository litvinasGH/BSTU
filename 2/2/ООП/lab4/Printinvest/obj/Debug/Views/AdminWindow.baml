   M S B A M L   `   `   `  ����F  BPrintinvest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Printinvest.ConvertersPrintinvest.Converters  5   	   #    Printinvest.Views.AdminWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5conv$clr-namespace:Printinvest.Converters     5   	     � Title$  Admin Window��5   	   $	��600��6   $
��1000��6+    � 	Resources 5      7   1Printinvest.Converters.TableToVisibilityConverter   TableToVisibilityConverter��%   &	          5   
   6*   5      .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��5q�6.   .+�5      �� $��Оборудование��6   $��	Equipment��6S   $��5q�6p   $	��120��6{   ��6,   �� *SwitchTableCommand+6{   �� 5      $��Услуги��6   $��Services��6M   $��5q�6i   $	��120��6t   ��6&   �� *SwitchTableCommand+6t   �� 5      $��Пользователи��6   $��Users��6S   $��5q�6l   $	��120��6w   ��6,   �� *SwitchTableCommand+6w   �� 5      $��Комментарии��6   $��Comments��6R   $��5q�6n   $	��120��6y   ��6+   �� *SwitchTableCommand+6y   5      &   System.Windows.Controls.DataGrid  5   
   $��1��6      AutoGenerateColumns .  ��6<   �� *EquipmentView+y�5      �� *SelectedTable+ �� 	Converter# [   �� ConverterParameter$ 	Equipment��   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%   	   
IsReadOnly	 . 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 61   �� *Id+6H     5      $ Модель��6%   
 65   �� *Model+  5      $ Цена��6%   
 63   �� *Price+4  .System.Windows.Controls.DataGridTemplateColumn  5      $ Действия��6)      CellTemplate 5      �� 5       .R�5!      �� $ 
Horizontal=�6)   .+�5"   "   �� $��Изменить��6)   $��2q�5#   F   ��5"   <   �� *$"DataContext.UpdateEquipmentCommand+ �� RelativeSource ��  �� AncestorType$ DataGridL���5#   )   �� 6F   �� 5$   "   $��Удалить��6)   $��2q�5%   F   ��5$   ;   �� *$"DataContext.DeleteEquipmentCommand+ �� $ DataGridL���5%   )   �� 6F   5&      5'      5)      5+        5.   
   $��1��6    .  ��6<   �� *ServicesView+y�5/      �� *SelectedTable+# [  $ Services�� 50        51      $ ID��6%   	 . 
 61   �� *	ServiceId+6O     52      $ Название��6%   
 67   �� *Name+  53      $ Цена��6%   
 63   �� *Price+  54      $ Действия��6)    55      �� 56      .R�57      �� $ 
Horizontal=�6)   .+�58   "   �� $��Изменить��6)   $��2q�59   F   ��58   <   �� *" DataContext.UpdateServiceCommand+ �� $ DataGridL���59   )   �� 6F   �� 5:   "   $��Удалить��6)   $��2q�5;   F   ��5:   ;   �� *" DataContext.DeleteServiceCommand+ �� $ DataGridL���5;   )   �� 6F   5<      5=      5?      5A        5D   
   $��1��6    .  ��6<   �� *	UsersView+y�5E      �� *SelectedTable+# [  $ Users�� 5F        5G      $ ID��6%   	 . 
 61   �� *UserId+6L     5H      $ 
Логин��6%   
 64   �� *Login+  5I      $ Пароль��6%   
 65   �� *
Password+  5J      $ Имя��6%   
 62   �� *Name+4  .System.Windows.Controls.DataGridCheckBoxColumn  5K      $ 
Админ��6)   
 68   �� *	IsAdmin+  5L      $ Действия��6)    5M      �� 5N      .R�5O      �� $ 
Horizontal=�6)   .+�5P   "   �� $��Изменить��6)   $��2q�5Q   F   ��5P   <   �� *DataContext.UpdateUserCommand+ �� $ DataGridL���5Q   )   �� 6F   �� 5R   "   $��Удалить��6)   $��2q�5S   F   ��5R   ;   �� *DataContext.DeleteUserCommand+ �� $ DataGridL���5S   )   �� 6F   5T      5U      5W      5Y        5\   
   $��1��6    .  ��6<   �� *CommentsView+y�5]      �� *SelectedTable+# [  $ Comments�� 5^        5_      $ ID��6%   	 . 
 61   �� *	CommentId+6O     5`      $ Оборудование��6%   
 6;   �� *Id+  5a      $ 
Автор��6%   
 64   �� *Author+  5b      $ 
Текст��6%   
 64   �� *Text+5d      �� 5g   
   $��Добавить��6   $��5q�6$   $	��100��6/   $
��Left=�6;   $��1��6V   $	��Top=�6c   ��5h      �� 5i       �� 
TargetType" ��6   .�5j      ��  �� Property � y� �� Value$ 	Collapsed=�63    �� Triggers 5k      �� 5l       �� Binding 6&   �� *SelectedTable+ �� Value 	Equipment6H   .a�5m      ��  � �� 68   �� *AddEquipmentCommand+�� 5n       � y�$ Visible=�6;   5o      �� 5p       6&   �� *SelectedTable+ Services6H   .a�5q      ��  � �� 68   �� *AddServiceCommand+�� 5r       � y�$ Visible=�6;   5s      �� 5t       6&   �� *SelectedTable+	 Users6H   .a�5u      ��  � �� 68   �� *AddUserCommand+�� 5v       � y�$ Visible=�6;   5w      5y      5{      5|      5}      