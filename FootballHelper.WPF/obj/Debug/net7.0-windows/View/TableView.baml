   M S B A M L   `   `   `  ����M  IFootballHelper.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:FootballHelper.WPF.ViewFootballHelper.WPF.View  5      '    !FootballHelper.WPF.View.TableView   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:FootballHelper.WPF.View     5        � 	Resources  5
         bvc��%   &	        �� 5   
   6'   5      .��5      �  � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $	��100 �6   .+�5   
   �� $��0��6   $
��Auto=�6$   .��5      �� $��20(�5      $��Arial)�5      
������ $
��Auto��5      $
��Auto��5      $��Stretch=�5      $	��Top=�5      ��5      �� *Clubs+ �� SelectedItem 5      �� *SelectedClub+5      5!      �� 5#   
    �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��1��6K   .+�5$      �� $��Состав��6   $��50��5'      $	��250��5(      $��5q�5)      $��Arial)�5*      $��24(�5+      $
��Bold"�5,      
������ 
���   ���5%      �� *
ShowClub+y�5&      �� *	IsVisible+ �� 	Converter# [  5.      �� 5/      $��Статистика��6   $��50��52      $	��250��53      $��5q�54      $��Arial)�55      $��24(�56      $
��Bold"�57      
������ 
���   ���50      �� *	ShowStats+y�51      �� *	IsVisible+# [  59      �� 5:      $%��Добавить команду��6   $��50��5<      $	��250��5=      $��5q�5>      $��Arial)�5?      $��24(�5@      $
��Bold"�5A      
������ 
���   ���5;      �� *	AddClub+5C      5D      5E      5H      