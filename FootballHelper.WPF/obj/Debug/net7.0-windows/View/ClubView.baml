   M S B A M L   `   `   `  ����M  IFootballHelper.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:FootballHelper.WPF.ViewFootballHelper.WPF.View  5      &     FootballHelper.WPF.View.ClubView   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:FootballHelper.WPF.View     5        � 	Resources  5
         bvc��%   &	        �� 5   
   6'   5      .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��9* �6   �� 5      $	��150 �6   .+�5      �� $��0��5      $��Center=�5      $��5q�5      $��28(�5      $
��Bold"�5      
���   �$��Arial)�5      ��5      �� *Name+5      �� 5      $
��Auto��6   $
��Auto��6)   $
��Auto=�66   $��1��6Y   .��5      �� $��14(�5"      $��Arial)�5#      
������ ��5       �� *	Players+ �� SelectedItem 5!      �� *SelectedPlayer+5$      5%      �� 5'   
    �� Orientation$ Vertical=�6   $��Center=�6,   $��Center=�6I   $��2��6d   .+�5(      �� $ 
Horizontal=�6   $��Center=�62   .+�5)      �� $$��Перейти к игроку��6   $��50��5,      $	��250��5-      $��10q�5.      $��Arial)�5/      $��18(�50      $
��Bold"�51      
������ 
���   ���5*      �� *
ShowPlayer+y�5+      �� *	IsVisible+ �� 	Converter# [  53      �� 55      $!��Удалить игрока��6   $��50��58      $	��250��59      $��10q�5:      $��Arial)�5;      $��18(�5<      $
��Bold"�5=      
������ 
���   ���56      �� *DeletePlayer+y�57      �� *	IsVisible+# [  5?      5@      �� 5B      $#��Добавить игрока��6   $��50��5D      $	��250��5E      $��10q�5F      $��Arial)�5G      $��18(�5H      $
��Bold"�5I      
������ 
���   ���5C      �� *	AddPlayer+5K      5L      5M      5O      