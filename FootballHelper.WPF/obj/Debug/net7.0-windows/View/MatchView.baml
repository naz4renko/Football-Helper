   M S B A M L   `   `   `  ����M  IFootballHelper.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:FootballHelper.WPF.ViewFootballHelper.WPF.View  5      '    !FootballHelper.WPF.View.MatchView   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:FootballHelper.WPF.View     5        � 	Resources  5	         bvc��%   &	        �� 5
   
   6'   5      .��5      �  � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $	��140 �6   .+�5   
   �� $
��Auto=�6   $��0��6:   .��5      �� 
������ ��5      �� *	Matches+ �� SelectedItem 5      �� *SelectedMatch+5      5      �� 5   
    �� Orientation$ Vertical=�6   $��Center=�6,   $��1��6I   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��Статистика��6   $��50��5       $	��250��5!      $��10q�5"      $��Arial)�5#      $��18(�5$      $
��Bold"�5%      
������ 
���   ���5      �� *	ShowStats+y�5      �� *	IsVisible+ �� 	Converter# [  5'      �� 5)      $��Удалить матч��6   $��50��5,      $	��250��5-      $��10q�5.      $��Arial)�5/      $��18(�50      $
��Bold"�51      
������ 
���   ���5*      �� *DeleteMatch+y�5+      �� *	IsVisible+# [  53      54      �� 56      $��Добавить матч��6   $��50��58      $	��250��59      $��10q�5:      $��Arial)�5;      $��18(�5<      $
��Bold"�5=      
������ 
���   ���57      �� *
AddMatch+5?      5@      5A      5D      