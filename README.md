# Shameless
3DS Database parser + ticket generator + QR code generator

# Disclaimer
I am not liable for whatever you choose to do with this program. It's only made to make reacquiring owned games easier. In the end it's your personal choice to use this software.

# What's it for?
This app lets you generate a valid ticket + FBI-compatible QR Code for any title or multiple titles in the list.


# Screenshots
![Screenshot 1](http://i.imgur.com/dFyi6lv.png)

![Screenshot 2](http://i.imgur.com/jFabMKw.png)

# Requirements
* .NET Framework 4.5.2 (unfortunately this means that **Windows XP will not be supported**. Sorry!)
* An internet connection
* FBI 2.2+

# Usage
1. Launch the program and let it prepare the database. Shouldn't take long, it's about a ~0.3MB download.
2. Select a title (or multiple titles) and press "Generate QR Code for FBI".
3. Scan the QR code with FBI's QR Code install option in the main menu, it will hopefully install the ticket. It will then ask if you would like to install the contents from the Nintendo CDN. Click "Yes".

# Credits
[FunKeyCia](https://github.com/llakssz/FunKeyCIA/blob/master/FunKeyCIA.py#L162-L204) - for CDN info code I translated from Python
