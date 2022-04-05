package com.example.myatb;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity {

    private TextView tvInfo;
    private EditText editText;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        tvInfo=findViewById(R.id.tvInfo);
        editText=findViewById(R.id.editTextData);
    }

    public void  handleClick(View view){
        String text=editText.getText().toString();
        tvInfo.setText(text);
    }
}