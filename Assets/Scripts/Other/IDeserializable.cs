using System.Collections;
using System.Collections.Generic;

public interface IDeserializable{
  string Hash();
  void SetProperty(string property);
}
