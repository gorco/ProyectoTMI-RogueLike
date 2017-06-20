using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public class DoorData {
  //  [XmlAttribute]
    //public int ID { get; set; }
    public string nameofnextroom;
    public string thisroom;
    public int next_room;
    public bool closed;
    public bool exist;
    public bool entered = false;
    public int place;

   
}

//[XmlType(TypeName = "Persons")]
public class DoorsData : IEnumerable<DoorData>
{
    private List<DoorData> inner = new List<DoorData>();

    public void Add(object o)
    {
        inner.Add((DoorData)o);
    }

    public IEnumerator<DoorData> GetEnumerator()
    {
        return inner.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
