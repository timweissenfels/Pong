   M S B A M L   `   `   `  ����F  BDlgMenuDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DlgMenuDemoDlgMenuDemo  5   	       Pong.StartDlg   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DlgMenuDemo     5   	     � Title$
  PONG��5   	   $��193.856��6   $��263.136��6'    � 
ResizeMode$ CanMinimize=�67   .��5	      �� $��	0,0,0,147q�6   .+�5
   
   �� $��Radius��6   $
��Left=�6!   $��	17,45,0,0q�6<   $	��Top=�6O   �� 5   
   -   5         ��Name$ radius��5      $
��Left=�6"   $��23��6=    �� TextWrapping$
 Wrap=�6I   $	��Top=�6]   $��35��6u    �� Left$ 71��6�   
 �� Top$ 48��6�   $��10��6�   �� 5   
   -   5         ��Name$ OK��5      $��OK��6   $ 17��6*   $	 116��6;   $��75��6L    �� 	IsDefault . 6h   �� 5   
   -   5        $ cancel��5      $��Cancel��6!   $	 158��62   $	 116��6D   $��75��6U    �� IsCancel . 6`   �� 5   
   $��(0 < Radius < 101)��6   $	 123��63   $ 45��6E   5      5      