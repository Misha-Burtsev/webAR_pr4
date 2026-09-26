mergeInto(LibraryManager.library, {
  // Расстояние между двумя пальцами на экране во время AR-сессии.
  // Второй палец в WebXR не создаёт событий касания, поэтому читаем
  // оба пальца напрямую из списка источников ввода сессии.
  // Возвращает 0, если пальцев на экране меньше двух.
  GetPinchDistance: function () {
    var session = Module.WebXR.xrSession;
    if (!session) return 0;

    var fingers = [];
    for (var i = 0; i < session.inputSources.length; i++) {
      var source = session.inputSources[i];
      if (source.targetRayMode == "screen" && source.gamepad) {
        fingers.push(source.gamepad.axes);
      }
    }
    if (fingers.length < 2) return 0;

    // Координаты пальцев от -1 до 1 по ширине и высоте экрана
    var dx = fingers[0][0] - fingers[1][0];
    var dy = fingers[0][1] - fingers[1][1];
    return Math.sqrt(dx * dx + dy * dy);
  }
});
