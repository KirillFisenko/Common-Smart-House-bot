using Newtonsoft.Json;

namespace Common_Smart_House_bot_common.YandexIotApiClient
{
    public class UserInfoRequest
    {

        public class Rootobject
        {
            public string status { get; set; }
            public string request_id { get; set; }
            public Room[] rooms { get; set; }
            public Group[] groups { get; set; }
            public Device[] devices { get; set; }
            public Scenario[] scenarios { get; set; }
            public Household[] households { get; set; }
        }

        public class Room
        {
            public string id { get; set; }
            public string name { get; set; }
            public object[] aliases { get; set; }
            public string household_id { get; set; }
            public string[] devices { get; set; }
        }

        public class Group
        {
            public string id { get; set; }
            public string name { get; set; }
            public object[] aliases { get; set; }
            public string household_id { get; set; }
            public string type { get; set; }
            public string[] devices { get; set; }
            public Capability[] capabilities { get; set; }
        }

        public class Capability
        {
            public bool retrievable { get; set; }
            public string type { get; set; }
            public Parameters parameters { get; set; }
            public State state { get; set; }
        }

        public class Parameters
        {
            public string color_model { get; set; }
            public Temperature_K temperature_k { get; set; }
            public Color_Scene color_scene { get; set; }
            public bool split { get; set; }
            public string instance { get; set; }
            public string unit { get; set; }
            public bool random_access { get; set; }
            public bool looped { get; set; }
            public Range range { get; set; }
        }

        public class Temperature_K
        {
            public int min { get; set; }
            public int max { get; set; }
        }

        public class Color_Scene
        {
            public Scene[] scenes { get; set; }
        }

        public class Scene
        {
            public string id { get; set; }
        }

        public class Range
        {
            public int min { get; set; }
            public int max { get; set; }
            public int precision { get; set; }
        }

        public class State
        {
            public string instance { get; set; }
            public object value { get; set; }
        }

        public class Device
        {
            public string id { get; set; }
            public string name { get; set; }
            public object[] aliases { get; set; }
            public string type { get; set; }
            public string external_id { get; set; }
            public string skill_id { get; set; }
            public string household_id { get; set; }
            public string room { get; set; }
            public string[] groups { get; set; }
            public Capability1[] capabilities { get; set; }
            public Property1[] properties { get; set; }
            public Quasar_Info quasar_info { get; set; }
        }

        public class Quasar_Info
        {
            public string device_id { get; set; }
            public string platform { get; set; }
            public string device_color { get; set; }
        }

        public class Capability1
        {
            public bool reportable { get; set; }
            public bool retrievable { get; set; }
            public string type { get; set; }
            public Parameters1 parameters { get; set; }
            public State1 state { get; set; }
            public float state_changed_at { get; set; }
            public float last_updated { get; set; }
        }

        public class Parameters1
        {
            public string instance { get; set; }
            public string unit { get; set; }
            public bool random_access { get; set; }
            public bool looped { get; set; }
            public Range1 range { get; set; }
            public string color_model { get; set; }
            public Temperature_K1 temperature_k { get; set; }
            public Color_Scene1 color_scene { get; set; }
            public bool split { get; set; }
            public Mode[] modes { get; set; }
        }

        public class Range1
        {
            public int min { get; set; }
            public int max { get; set; }
            public int precision { get; set; }
        }

        public class Temperature_K1
        {
            public int min { get; set; }
            public int max { get; set; }
        }

        public class Color_Scene1
        {
            public Scene1[] scenes { get; set; }
        }

        public class Scene1
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Mode
        {
            public string value { get; set; }
        }

        public class State1
        {
            public string instance { get; set; }
            public object value { get; set; }
        }

        public class Property1
        {
            public string type { get; set; }
            public bool reportable { get; set; }
            public bool retrievable { get; set; }
            public Parameters2 parameters { get; set; }
            public State2 state { get; set; }
            public float state_changed_at { get; set; }
            public float last_updated { get; set; }
        }

        public class Parameters2
        {
            public string instance { get; set; }
            public string unit { get; set; }
            public Event[] events { get; set; }
        }

        public class Event
        {
            public string value { get; set; }
        }

        public class State2
        {
            public string instance { get; set; }
            public object value { get; set; }
        }

        public class Scenario
        {
            public string id { get; set; }
            public string name { get; set; }
            public bool is_active { get; set; }
            public Step[] steps { get; set; }
            public Trigger[] triggers { get; set; }
            public Notifications notifications { get; set; }
        }

        public class Notifications
        {
            public Sms sms { get; set; }
            public Phone_Call phone_call { get; set; }
            public Push push { get; set; }
        }

        public class Sms
        {
        }

        public class Phone_Call
        {
        }

        public class Push
        {
        }

        public class Step
        {
            public string type { get; set; }
            public Parameters3 parameters { get; set; }
        }

        public class Parameters3
        {
            public Launch_Devices[] launch_devices { get; set; }
            public object[] requested_speaker_capabilities { get; set; }
            public object stereopairs { get; set; }
            public int delay_ms { get; set; }
            public Item[] items { get; set; }
        }

        public class Launch_Devices
        {
            public string id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public Capability2[] capabilities { get; set; }
            public string skill_id { get; set; }
        }

        public class Capability2
        {
            public bool reportable { get; set; }
            public bool retrievable { get; set; }
            public string type { get; set; }
            public Parameters4 parameters { get; set; }
            public State3 state { get; set; }
            public float state_changed_at { get; set; }
            public float last_updated { get; set; }
        }

        public class Parameters4
        {
            public string instance { get; set; }
            public bool split { get; set; }
            public string[] instance_names { get; set; }
        }

        public class State3
        {
            public string instance { get; set; }
            public object value { get; set; }
            public bool relative { get; set; }
        }

        public class Item
        {
            public string id { get; set; }
            public string type { get; set; }
            public Value value { get; set; }
        }

        public class Value
        {
            public string id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public Capability3[] capabilities { get; set; }
            public string skill_id { get; set; }
        }

        public class Capability3
        {
            public bool reportable { get; set; }
            public bool retrievable { get; set; }
            public string type { get; set; }
            public Parameters5 parameters { get; set; }
            public State4 state { get; set; }
            public float state_changed_at { get; set; }
            public float last_updated { get; set; }
        }

        public class Parameters5
        {
            public string instance { get; set; }
            public bool split { get; set; }
        }

        public class State4
        {
            public string instance { get; set; }
            public object value { get; set; }
            public bool relative { get; set; }
        }

        public class Trigger
        {
            public Trigger1 trigger { get; set; }
            public object[] filters { get; set; }
        }

        public class Trigger1
        {
            public string type { get; set; }
            public object value { get; set; }
        }

        public class Household
        {
            public string id { get; set; }
            public string name { get; set; }
            public object[] aliases { get; set; }
            public string type { get; set; }
        }


        public static Rootobject GetYandexUserInfo()
        {
            string Request = YandexApiClient.GetUserInfoAsync().Result;
            return JsonConvert.DeserializeObject<Rootobject>(Request);
        }
    }
}