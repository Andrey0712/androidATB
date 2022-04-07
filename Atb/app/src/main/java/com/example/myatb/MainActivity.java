package com.example.myatb;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

import com.example.myatb.network.account.AccountService;
import com.example.myatb.network.account.dto.AccountResponseDTO;
import com.example.myatb.network.account.dto.RegisterDto;
import com.example.myatb.network.account.dto.RegisterErrorDTO;
import com.example.myatb.network.account.dto.ValidationRegisterDTO;
import com.google.gson.Gson;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

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
//        String text=editText.getText().toString();
//        tvInfo.setText(text);
        RegisterDto registerDto=new RegisterDto();
        registerDto.setEmail("ss@ss.ss");
        AccountService.getInstance()
                .jsonApi()
                .register(registerDto)
                .enqueue(new Callback<AccountResponseDTO>() {
                    @Override
                    public void onResponse(Call<AccountResponseDTO> call, Response<AccountResponseDTO> response) {
                        if(response.isSuccessful()){
                            AccountResponseDTO data= response.body();
                            tvInfo.setText("response is good");
                        }
                        try {
                            String json=response.errorBody().string();
                            Gson gson=new Gson();
                            ValidationRegisterDTO result =gson.fromJson(json, ValidationRegisterDTO.class);
                            RegisterErrorDTO registerErrorDTO=(RegisterErrorDTO) result.getErrors();
                            int t=3;

                        }catch (Exception e){
                            System.out.println("Error response parse body");
                        }

                    }


                    @Override
                    public void onFailure(Call<AccountResponseDTO> call, Throwable t) {
                      String str=t.toString();
                       int a=12;
                    }
                });
    }
}