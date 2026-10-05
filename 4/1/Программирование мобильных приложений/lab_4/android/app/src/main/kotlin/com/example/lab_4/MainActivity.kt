package com.example.lab_4

import android.content.Context
import android.content.ContextWrapper
import android.content.Intent
import android.content.IntentFilter
import android.hardware.Sensor
import android.hardware.SensorEvent
import android.hardware.SensorEventListener
import android.hardware.SensorManager
import android.net.Uri
import android.os.BatteryManager
import android.os.Build.VERSION
import android.os.Build.VERSION_CODES
import io.flutter.embedding.android.FlutterActivity
import io.flutter.embedding.engine.FlutterEngine
import io.flutter.plugin.common.MethodChannel

class MainActivity : FlutterActivity() {

    private val GYROSCOPE_CHANNEL = "gyroscope"
    private val BROWSER_CHANNEL = "browser"

    private val BAT_CHANNEL = "battery"

    override fun configureFlutterEngine(flutterEngine: FlutterEngine) {
        super.configureFlutterEngine(flutterEngine)

        // 1. Канал для гироскопа
        MethodChannel(flutterEngine.dartExecutor.binaryMessenger, GYROSCOPE_CHANNEL)
            .setMethodCallHandler { call, result ->
                if (call.method == "getGyroscope") {
                    val sensorManager = getSystemService(Context.SENSOR_SERVICE) as SensorManager
                    val gyroscope = sensorManager.getDefaultSensor(Sensor.TYPE_GYROSCOPE)

                    if (gyroscope == null) {
                        result.error("NO_GYROSCOPE", "Гироскоп не найден", null)
                        return@setMethodCallHandler
                    }

                    val listener = object : SensorEventListener {
                        override fun onSensorChanged(event: SensorEvent) {
                            val data = mapOf(
                                "x" to event.values[0],
                                "y" to event.values[1],
                                "z" to event.values[2]
                            )
                            sensorManager.unregisterListener(this)
                            result.success(data)
                        }
                        override fun onAccuracyChanged(sensor: Sensor?, accuracy: Int) {}
                    }
                    sensorManager.registerListener(listener, gyroscope, SensorManager.SENSOR_DELAY_NORMAL)
                } else {
                    result.notImplemented()
                }
            }

        // 2. Канал для открытия веб-браузера
        MethodChannel(flutterEngine.dartExecutor.binaryMessenger, BROWSER_CHANNEL)
            .setMethodCallHandler { call, result ->
                if (call.method == "openBrowser") {
                    val url = call.arguments as? String
                    if (url != null) {
                        try {
                            val intent = Intent(Intent.ACTION_VIEW, Uri.parse(url))
                            context.startActivity(intent)
                            result.success(null) 
                        } catch (e: Exception) {
                            result.error("CANT_OPEN_URL", "Не удалось открыть ссылку: ${e.message}", null)
                        }
                    } else {
                        result.error("BAD_ARGUMENTS", "Ссылка не была передана или имеет неверный формат", null)
                    }
                } else {
                    result.notImplemented()
                }
            }

        MethodChannel(flutterEngine.dartExecutor.binaryMessenger, BAT_CHANNEL).setMethodCallHandler {
            call, result ->
            if (call.method == "getBatteryLevel") {
                val batteryLevel = getBatteryLevel()

                if (batteryLevel != -1) {
                result.success(batteryLevel)
                } else {
                result.error("UNAVAILABLE", "Battery level not available.", null)
                }
            } else {
                result.notImplemented()
            }
            }



    }
      private fun getBatteryLevel(): Int {
    val batteryLevel: Int
    if (VERSION.SDK_INT >= VERSION_CODES.LOLLIPOP) {
      val batteryManager = getSystemService(Context.BATTERY_SERVICE) as BatteryManager
      batteryLevel = batteryManager.getIntProperty(BatteryManager.BATTERY_PROPERTY_CAPACITY)
    } else {
      val intent = ContextWrapper(applicationContext).registerReceiver(null, IntentFilter(Intent.ACTION_BATTERY_CHANGED))
      batteryLevel = intent!!.getIntExtra(BatteryManager.EXTRA_LEVEL, -1) * 100 / intent.getIntExtra(BatteryManager.EXTRA_SCALE, -1)
    }

    return batteryLevel
  }

}


