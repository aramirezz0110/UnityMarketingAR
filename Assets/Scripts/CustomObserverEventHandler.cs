using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
//SCRIPT 'CustomObserverEventHandler' PERSONALIZADO
public class CustomObserverEventHandler : DefaultObserverEventHandler
{
    //variables para la reproduccion del video
    //para cargar el video y luego reproducirlo
    public bool isVideoEnabled;
    [Space]
    public GameObject videoGO; //referencia general

    private Button interactionButton;
    private VideoPlayer videoPlayer;
    private RawImage rawImage; //para proyectar el video sobre la imagen
    //variables para abrir la url haciendo clic sobre el video
    
    public string url;
    private void Awake()
    {
        interactionButton = videoGO.GetComponent<Button>();
        videoPlayer = videoGO.GetComponent<VideoPlayer>();
        rawImage = videoGO.GetComponent<RawImage>();
    }
    protected override void Start()
    {
        base.Start();
        //elemento de escucha para el evento onclick
        interactionButton.onClick.AddListener(OpenUrl);
        //corrutina para la carga del video
        StartCoroutine(PrepareVideo());
    }
    private IEnumerator PrepareVideo()
    {
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
        {
            //mantener en un bucle hasta que el video este cargado
            yield return new WaitForSeconds(.5f);
        }

        rawImage.texture = videoPlayer.texture;
        //habilitacion del video
        isVideoEnabled = true;
    }
    protected override void OnTrackingFound()
    {
        //llamada al metodo del padre
        base.OnTrackingFound();
        //volver a reproducir el video
        if (isVideoEnabled) videoPlayer.Play();
        
    }
    //SOBREESCRITURA DEL METODO PADRE
    protected override void OnTrackingLost()
    {
        //llamada al metodo del padre
        base.OnTrackingLost();
        //pausar el video
        if (isVideoEnabled) videoPlayer.Pause();
    }
    private void OpenUrl()
    {
        Application.OpenURL(url);
    }
}
