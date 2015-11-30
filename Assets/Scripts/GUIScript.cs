using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {
	
	public GUISkin				skin;
	
	
	private Rect				lifeCounter;
	
	private HealthScript	controller;
	
	void Awake ()
	{
		// Vi hämtar en referens till Healtht-scriptet som också ligger på det här objektet (titta på Player i scenen)
		controller = GetComponent<HealthScript>();
		
		
		
		lifeCounter = new Rect( 
		                       -50, 								// Placering på skärmen, i sidled
		                       Screen.height - 60, 				// Här använder vi helt enkelt hela skärmhöjden minus texthöjden, eftersom olika skärmar är olika stora
		                       100, 							// Rektangelns bredd
		                       20		 						// Rektangelns höjd
		                       );
	}
	
	void OnGUI ()
	{
		// Det här måste vi göra för att ändringarna vi gjort i vårt egna GUISkin ska ha någon effekt
		if ( skin != null )
			GUI.skin = skin;
		
		// Om det tar slut på liv, eller du har plockat alla pickups så kommer menyn visas
		if ( controller.hp <= 0 )
			transform.parent.gameObject.AddComponent<GameOverScript> ();
		
		
		// Om du antingen dött eller vunnit så ska inte resten av menyn visas: detta kallas för en "early return"
		if ( controller.hp <= 0 )
			return;
		
		
		// Det här är livsmätaren
		GUI.Label( lifeCounter, "" );
		
		// Först skapar vi en ny rektangel, som vi gör exakt likadan som den för livsmätaren
		Rect rect = lifeCounter;
		
		// Sedan går vi igenom varje liv vi har kvar
		for ( int hp = 1; hp <= controller.hp; ++ hp )
		{
			// ...och för varje liv vi HAR kvar så görs följande:
			rect.width = 40;
			rect.height = 40;
			
			// Vi sätter x-värdet (positionen i sidled) till exakt detsamma som livsmätaren
			rect.x = lifeCounter.x;
			
			// Vi ökar på detta värde med livsmätarens maximala värde i sidled
			rect.x += lifeCounter.xMax;
			
			// Vi lägger sedan till en multiplikation av vilket liv i ordningen det här är och hur bred den förväntas vara
			// Det senare här - fixedWidth - är bestämt i vårt GUISkin
			rect.x += ( hp * 30 );
			
			// Slutligen ritar vi en låda med de här måtten
			GUI.Box( rect, "" );
		}
	}
	
	
	
}
