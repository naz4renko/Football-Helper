   M S B A M L   `   `   `  ����M  IFootballHelper.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:FootballHelper.WPF.ViewFootballHelper.WPF.View  5      -    'FootballHelper.WPF.View.CreateMatchView   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:FootballHelper.WPF.View     5        � 	Resources  5	         bvc��%   &	        �� 5
   
   6'   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   .+�5   
   �� $!��Создание матча��6   $��Center=�5      $��20q�5      $��28(�5      $
��Bold"�5      
���   �$��Arial)�5      $��2��5      �� 5   
   $��Center=�6   $��0��60   .+�5      �� $!��Команда-Хозяин��6   $��Center=�5      $��7q�5      $��24(�5       $
��Bold"�5!      
���   �$��Arial)�5#      �� 5$      $
��Auto��5%      $	��300��5&      $��Stretch=�5'      $	��Top=�5(      .��5)      �� $��18(�5,      $��Arial)�5-      ���	#FFC7E5FF$	��200��5/      $	��300��6"   
���   ���5*      �� *Clubs+ �� SelectedItem 5+      �� *SelectedHomeClub+50      51      52      �� 55   
   $��Center=�6   $��1��60   .+�56      �� $��Команда-Гость��6   $��Center=�57      $��7q�58      $��24(�59      $
��Bold"�5:      
���   �$��Arial)�5<      �� 5=      $
��Auto��5>      $	��300��5?      $��Stretch=�5@      $	��Top=�5A      .��5B      �� $��14(�5E      $��Arial)�5F      ���	#FFC7E5FF$	��200��5H      $	��300��6"   
���   ���5C      �� *Clubs+ 5D      �� *SelectedAwayClub+5I      5J      5K      �� 5M   
    �� Orientation$ Vertical=�6   $��2��6,   $��Center=�6@   $��Center=�6]   .+�5N      �� $#��Дата проведения��6   $��18(�5O      $
��Bold"�5P      
���   �$��Arial)�5R      $��5q�5S      (  "System.Windows.Controls.DatePicker  5T         SelectedDate 6   �� *DateOfTheMatch+5U      �� 5W   
   $ 
Horizontal=�6   $��2��6.   $��Center=�6B   $��Bottom=�6_   $��0,0,0,30q�6z   .+�5X      �� $��
Далее��6   $��50��5[      $	��250��5\      $��10q�5]      $��Arial)�5^      $��18(�5_      $
��Bold"�5`      
������ ��5Y      �� *ComfirmCreate+y�5Z      �� *	IsVisible+ �� 	Converter# [  ��5b      �� *ButtonTextColor+�� 5d      $��
Назад��6   $��50��5f      $	��250��5g      $��10q�5h      $��Arial)�5i      $��18(�5j      $
��Bold"�5k      
������ 
���   ���5e      �� *	ToMatches+5m      5n      5o      5p      