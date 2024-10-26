using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {

        soundSource = GetComponent<AudioSource>();
        //lấy object MusicSource
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        //Giữ object này ngay cả khi chuyển sang cảnh mới
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //Hủy các object trùng lặp
        else if (instance != null && instance != this)
            Destroy(gameObject);

        //Assign lại giá trị cho sound và music về 0 trước khi bắt đầu lại game
        ChangeMusicVolume(0);
        ChangeSoundVolume(0);
    }
    public void PlaySound(AudioClip _sound)
    {
        //phát đoạn âm thanh chỉ 1 lần
        soundSource.PlayOneShot(_sound);
    }

    //Thay đổi volume
    public void ChangeSoundVolume(float _change)
    {
        ChangeSourceVolume(1, "soundVolume", _change, soundSource);
    }

    //thay đổi music volume
    public void ChangeMusicVolume(float _change)
    {
        ChangeSourceVolume(0.3f, "musicVolume", _change, musicSource);
    }

    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source)
    {
        //base volume

        //Lấy giá trị ban đầu của volume và thay đổi
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);//load lại giá trị volume đã lưu trước đó ở trong player prefs
        currentVolume += change;

        //Kiểm tra xem volume đã đạt đến giá trị lớn nhất hay nhỏ nhất chưa
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        //Assign final value
        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        //Lưu final value cho player prefs
        //Player prefs dùng để lưu các giá trị như float, string, int cho user's platform registry
        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }

}
