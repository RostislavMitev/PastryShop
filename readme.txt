Коледен магазин за сладки

Има различни будки, които могат да приемат различно количество деликатеси и коктейли
Будките имат id, капацитет, меню с деликатеси, меню с коктейли, текуща сметка, оборот, състояние на резервация (да/не)
Деликатесите имат име и цена. Коктейлите имат име, количество и цена.

Има различни команди:
 - AddBooth - добавяне на будка (капацитет)
 - AddDelicacy - добавяне на деликатес (id, тип деликатес, име на деликатеса)
 - AddCocktail - добавяне на коктейл (id, тип коктейл, име на коктейла, количество)
 - ReverseBooth - резервиране на будка (брой хора)
 - TryOrder - поръчване (id, поръчка(разделени в следния стил - тип(коктейл или деликатес)/име на коктейла или деликатеса/брой/{опционално, ако избрания тип е коктейл}))
 - LeaveBooth - напускане на будката (id) - извежда текущата сметка и показва, че будката е свободна
 - BoothReport - извежда данните за будката (id) - капацитет, оборот, коктейлите и деликатесите в менюто на конкретната будка
 - Exit - прекратяване на програмата



Примерен input:
AddBooth 5
AddBooth 3
AddBooth 3
AddDelicacy 1 Stolen Sugarcookie
AddDelicacy 2 Gingerbread Dwarfhat
AddCocktail 3 Hibernation Bluewater Large
AddCocktail 3 Hibernation Bluewater Small
ReserveBooth 2
ReserveBooth 6
ReserveBooth 3
TryOrder 3 Hibernation/Bluewater/3/Middle
TryOrder 2 Stolen/Sugarcookie/1
LeaveBooth 3
LeaveBooth 2
BoothReport 1
BoothReport 2
BoothReport 3
Exit

Output на примерния input:
Added booth number 1 with capacity 5 in the pastry shop!
Added booth number 2 with capacity 3 in the pastry shop!
Added booth number 3 with capacity 3 in the pastry shop!
Stolen Sugarcookie added to the pastry shop!
Gingerbread Dwarfhat added to the pastry shop!
Large Bluewater Hibernation added to the pastry shop!
Small Bluewater Hibernation added to the pastry shop!
Booth 3 has been reserved for 2 people!
No available booth for 6 people!
Booth 2 has been reserved for 3 people!
There is no Middle Bluewater available!
There is no Stolen Sugarcookie available!
Bill 0.00 lv
Booth 3 is now available!
Bill 0.00 lv
Booth 2 is now available!
Booth: 1
Capacity: 5
Turnover: 0.00 lv
-Cocktail menu:
-Delicacy menu:
--Sugarcookie - 3.50 lv
Booth: 2
Capacity: 3
Turnover: 0.00 lv
-Cocktail menu:
-Delicacy menu:
--Dwarfhat - 4.00 lv
Booth: 3
Capacity: 3
Turnover: 0.00 lv
-Cocktail menu:
--Bluewater (Large) - 10.50 lv
--Bluewater (Small) - 3.50 lv
-Delicacy menu:
