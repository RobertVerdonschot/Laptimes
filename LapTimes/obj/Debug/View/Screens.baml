   M S B A M L   `   `   `  ����C  ?LapTimes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:LapTimes.ViewModelLapTimes.ViewModel  5      7 clr-namespace:LapTimes.ViewModelLapTimes.ViewModel  5	          LapTimes.View.Screens   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6	viewmodel clr-namespace:LapTimes.ViewModel     5      2local clr-namespace:LapTimes.ViewModel     5	        � 	Resources  5      "   LapTimes.ViewModel.ScreensVM   	screensVM��%   &	          5   
   6,   5      .��5      �  � DataContext# [  6   .+�5   
   �� $��0,0,0,0q�6   $��Stretch=�6&   $��Stretch=�6D   $��8(�6`   .@�5      �� $
��Laps��6   $y�	Collapsed=�6$    �� 
IsSelected 5      �� *LapsScreenActive+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�.��5      �  � RowDefinitions 5      �� 5      $��50 �6(   �� 5      $��1* �6(   �� 5      $��50 �6(   .+�5      �� $��0��6!    �� Orientation$ 
Horizontal=�6.   .+�5      �� $��LTicon_s.png��6    $��20��66   $
��10,0q�6B   $
��Left=�5        $��Center=�6;   �� 5!      $��Ronden��6$   $��0��62   $	��0,0q�6?   $
��Left=�5"       $��Center=�6;   $��18(�6V   5#      �� 5'      $��2��6!   $ 
Horizontal=�6.   $��LeftToRight=�6G   .+�5(      �� -   5        $��Setup��5(   !   $��10,10q�61   $��75��6@   $��Stretch=�5)      $��Center=�69   $��16(�6V   6d   �� 5*      -   5        $��Analyse��5*   !   $��10,10q�63   $��75��6B   $��Stretch=�5+      $��Center=�69   $��16(�6V   6d   �� 5,      $��Track��6!   $��10,10q�61   $��75��6@   $y�Hidden=�6K   $��Stretch=�5-      $��Center=�69   $��16(�6V   5.      5/      50      �� 53      $��Setup��6   $y�	Collapsed=�6%    54      �� *SetupScreenActive+$ TwoWay=�$ PropertyChanged=�.��55      �  56      �� 57      $��50 �6(   �� 58      $��1* �6(   �� 59      $��50 �6(   .+�5<      �� $��0��6!   $ 
Horizontal=�6.   .+�5=      �� $��LTicon_s.png��6    $��20��66   $
��10,0q�6B   $
��Left=�5>       $��Center=�6;   �� 5?      $��Setup��6$   $��0��61   $	��0,0q�6>   $
��Left=�5@       $��Center=�6;   $��18(�6V   5A      �� 5E      $��2��6!   $ 
Horizontal=�6.   $��RightToLeft=�6G   .+�5F      �� -   5        $
��Back��5F   !   $��10,10q�60   $��75��6?   $��Stretch=�5G   !   $��Right=�6=   $��16(�6Y   5H   !   5I      5J      5K      �� 5N      $��Analysis��6   $y�	Collapsed=�6(    5O      �� *AnalysisScreenActive+$ TwoWay=�$ PropertyChanged=�.��5P      �  5Q      �� 5R      $��50 �6(   �� 5S      $��1* �6(   �� 5T      $��50 �6(   .+�5W      �� $��0��6!   $ 
Horizontal=�6.   .+�5X      �� $��LTicon_s.png��6    $��20��66   $
��10,0q�6B   $
��Left=�5Y       $��Center=�6;   �� 5Z      $��Analyse��6$   $��0��63   $	��0,0q�6@   $
��Left=�5[       $��Center=�6;   $��18(�6V   5\      �� 5`      $��2��6!   $ 
Horizontal=�6.   $��RightToLeft=�6G   .+�5a      �� -   5        $
��Back��5a   !   $��10,10q�60   $��75��6?   $��Stretch=�5b   !   $��Right=�6=   $��16(�6Y   5c   !   5d      5e      5f      5g      5j      5k      