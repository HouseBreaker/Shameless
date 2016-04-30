# Shameless
3DS Database parser + ticket generator + QR code generator

#Disclaimer
I am not liable for whatever you choose to do with this program. It's only made to make reacquiring owned games easier. In the end it's your personal choice to use this software.

# What's it for?
This repository actually houses 2 programs - Shameless and TicketGenerator.

* Shameless - a .NET GUI app which lets you generate a valid ticket + FBI-compatible QR Code for any game in the list.

* TicketGenerator - a .NET console app which lets you generate valid tickets for installation with FBI.

#Screenshots
![Screenshot 1](http://i.imgur.com/dFyi6lv.png)

![Screenshot 2](http://i.imgur.com/jFabMKw.png)

#Requirements
* .NET Framework 4.5.2 (unfortunately this means that **Windows XP will not be supported**. Sorry!)
* An internet connection
* FBI 2.1+

#Usage
1. Launch the program and let it prepare the database. Shouldn't take long, it's about a 1.5MB download.
2. Select a title and press "Generate QR Code for FBI". Optionally check "Show size?" to get info about how big the title is from the Nintendo CDN. (this will be either instant or it will take a long time, this is why I made it optional).
3. Scan the QR code with FBI's QR Code install option in the main menu, it will hopefully install the ticket.
4. Go to Tickets in FBI on the main screen, look up the TitleID of the title, select it and select Install from CDN

#Credits
[FunKeyCia](https://github.com/llakssz/FunKeyCIA/blob/master/FunKeyCIA.py#L162-L204) - for CDN info code I translated from Python
