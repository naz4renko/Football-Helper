   M S B A M L   `   `   `  ����M  IFootballHelper.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:FootballHelper.WPFFootballHelper.WPF  5   	   A%clr-namespace:FootballHelper.WPF.ViewFootballHelper.WPF.View  5   	   K*clr-namespace:FootballHelper.WPF.ViewModelFootballHelper.WPF.ViewModel  5   	   #    FootballHelper.WPF.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:FootballHelper.WPF     5   	   7views%clr-namespace:FootballHelper.WPF.View     5   	   9vm*clr-namespace:FootballHelper.WPF.ViewModel     5   	     � Title$  
MainWindow��5
   	   $	��600��6   $
��1000��6)    � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5       �� DataType1  +FootballHelper.WPF.ViewModel.TableViewModel'   !FootballHelper.WPF.View.TableView0   *FootballHelper.WPF.ViewModel.ClubViewModel&    FootballHelper.WPF.View.ClubView5   /FootballHelper.WPF.ViewModel.ClubShortViewModel+   %FootballHelper.WPF.View.ClubShortView1   +FootballHelper.WPF.ViewModel.MatchViewModel'   !FootballHelper.WPF.View.MatchView6	   0FootballHelper.WPF.ViewModel.MatchShortViewModel,
   &FootballHelper.WPF.View.MatchShortView6   0FootballHelper.WPF.ViewModel.MatchStatsViewModel,   &FootballHelper.WPF.View.MatchStatsView2   ,FootballHelper.WPF.ViewModel.PlayerViewModel(   "FootballHelper.WPF.View.PlayerView7   1FootballHelper.WPF.ViewModel.PlayerShortViewModel-   'FootballHelper.WPF.View.PlayerShortView6   0FootballHelper.WPF.ViewModel.CreateClubViewModel,   &FootballHelper.WPF.View.CreateClubView8   2FootballHelper.WPF.ViewModel.CreatePlayerViewModel.   (FootballHelper.WPF.View.CreatePlayerView7   1FootballHelper.WPF.ViewModel.CreateMatchViewModel-   'FootballHelper.WPF.View.CreateMatchView<   6FootballHelper.WPF.ViewModel.PlayerStatsShortViewModel2   ,FootballHelper.WPF.View.PlayerStatsShortView=   7FootballHelper.WPF.ViewModel.PlayerChangeStatsViewModel3   -FootballHelper.WPF.View.PlayerChangeStatsView:  4FootballHelper.WPF.ViewModel.PlayerAddStatsViewModel0   *FootballHelper.WPF.View.PlayerAddStatsView5   /FootballHelper.WPF.ViewModel.ClubStatsViewModel+   %FootballHelper.WPF.View.ClubStatsView<   6FootballHelper.WPF.ViewModel.MatchChangeStatsViewModel2    ,FootballHelper.WPF.View.MatchChanceStatsView��%�  (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)(�� �     5      *, +)(�� �     5      *,	 +)(�� �     5"      *, +)(�� ,    5&      *, +)(�� ^    5)      *, +)(�� �    5-      *, +)(�� �    50      *, +)(�� �    54      *, +)(�� &    58      *, +)(�� X    5<      *, +)(�� �    5@      *, +)(�� �    5D      *, +)(�� �    5H      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   " 	 6   .R�5      
  5       �� 5"   
   "  6   .R�5#        5$      �� 5&   
   "  6   .R�5'        5(      �� 5)   
   "  6   .R�5*        5+      �� 5-   
   "  6   .R�5.        5/      �� 50   
   "  6   .R�51        52      �� 54   
   "  6   .R�55        56      �� 58   
   "  6   .R�59        5:      �� 5<   
   "  6   .R�5=        5>      �� 5@   
   "  6   .R�5A        5B      �� 5D   
   "  6   .R�5E        5F      �� 5H   
   "  6   .R�5I         5J      5L      .��5N      � ���#88c8ff � ColumnDefinitions 5O   
   �� 5P      $��1* �6   �� 5Q      $��1* �6    � RowDefinitions 5S   
   �� 5T      $��60 �6   �� 5U      $��1* �6   .+�5X   
   �� $��Таблица��6   $��0��5Z      $��Arial)�5[      $��20(�5\      $
��Bold"�5]      
������ 
���   ���5Y      �� *	ShowTable+5_      �� 5`   
   $��
Матчи��6   $��1��5b      $��Arial)�5c      $��20(�5d      $
��Bold"�5e      
������ 
���   ���5a      �� *ShowMatches+5g      �� 5h   
   $��1��67   $��2��6D   ��6   �� *	Content+6D   5i      5j      