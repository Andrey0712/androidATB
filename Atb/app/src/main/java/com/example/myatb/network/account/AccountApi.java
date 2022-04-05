package com.example.myatb.network.account;

import com.example.myatb.network.account.dto.AccountResponseDTO;
import com.example.myatb.network.account.dto.RegisterDto;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface AccountApi {
    @POST("api/account/register")
    public Call<AccountResponseDTO> register(@Body RegisterDto model);
}
