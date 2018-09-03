Zanim zapomne, to tu jest niezly burdel, ale chyba jest fajnie

Zmienilem layery - jest teraz Actors i Transparent

Transparent to Layer, ktory pojawia sie, gdy Actors zostanie zasloniety przez cos, co nie ma Transparent
	- czyli na przyklad obiekt 3D bez transparent albo background

Konfiguracja cieni jest magiczna - ShadowWarriorPrototype_Final i Env_Final powinny dzialac

Background musi byc na teksturze

Zeby cienie nie przenikaly przez budynki, bedziemy musieli dodac jakis prymitywny obiekt, ktory
bedzie lekko wewnatrz budynkow i bedzie blokowal cienie (on niestety bedzie widoczny i dlatego
musi byc jakby pod budynkiem - z mniejsza depth mapa)
Obiekt bedzie mial cast shadow - off, recieive shadows - on