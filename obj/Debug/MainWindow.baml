   M S B A M L   `   `   `  ����F  BDlgMenuDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PongPong  5   	       Pong.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Pong     5   	   -   5          �Name$	  Wnd��5   	    � Title$ 
MainWindow��5   	   $	��800��6   $
��1200��6)    � 
ResizeMode$ NoResize=�6J   
��� d �.��5	      �� -   5         ��Name$	 Cvs��5	      .+�5
   
   o� -   5        $��MainMenu��5
       o� 
IsMainMenu . $
��Left=�61   $��21��6L   $	��Top=�6X   $��43��6p    �� Left$ 10��6{   
 �� Top$ 10��6�   .@�5      m� $��Spiel��6   $��43��6&   $��21��61   .@�5      m� -   5        $��start��5      $��Start��6(   67   m� 5      -   5        $
��ende��5      $
��Ende��6'   65   m� 5      -   5        $��	parameter��5      $��	Parameter��6,   6?   5      5      �� 5   
   -   5         ��Name$
 Rect��5      ���	#FFF4F4F5$	��680��63   $	 108��6@   
���   �$ 36��6a   $
��1007��6q   �� 5   
   -   5        $��Reset��5      $ 10��6!   $ 36��62   $��43��6B    J� RenderTransformOrigin$ 0.016,-0.142&�6M   6r   �� 5   
   $	��680��6   $	 108��6   $ 36��6/   $
��1007��6?   $��fussball-mittellinie.jpg��6L   $
��Fill=�6n   �� 5   
   -	   5        $ midrect��5      ���	#FFF4F4F5$	��680��67   $	 608��6D   
���   �$ 33��6e   $��1��6u   $ 0.5,0.5&�6   z�5      V� 5      .~�5      �� 	 �� ScaleX$	 -1Q�6%   �� 5      �� 5      T� 5      5      5      �� 5    
   -
   5        
 ��Name$
 score_s1��5       $��Score Spieler 1: ��6#   $	 152��6?   $ 5��6Q   $	��123��6`   
���  ��6l   �� 5!   
   -   5        $
 score_s1_value��5!      $��0��6)   $	 250��65   $ 5��6G   $	��123��6V   
���  ��6b   �� 5"   
   -   5        $
 score_s2��5"      $��Score Spieler 2: ��6#   $	 944��6?   $ 7��6Q   $	��123��6`   
���  ��6l   �� 5#   
   -   5        $
 score_s2_value��5#      $��0��6)   $
 1061��65   $ 7��6H   $	��123��6W   
���  ��6c   �� 5$   
   -   5        $	 497��5$      $ 8��6#   $ 	0.5,0.556&�62   $	��223��6T    � Maximum$ 50Q�6`    � Minimum$ 1Q�6m    � Value$ 10Q�6y    �� TickPlacement$ BottomRight=�6�    �� TickFrequency$ 5Q�6�   z�5%      V� 5&      .~�5'      �� �� 5(       �� AngleY$
 0.05Q�6$   �� 5)       �� Angle$ -0.064Q�6&   T� 5*       T� Y$ 0.064Q�6)   5+      5-      �� 5.   
   $��Ball Geschwindigkeit:��6   $	 353��60   $ 5��6B   $	��142��6Q   
���  ��6]   5/      51      